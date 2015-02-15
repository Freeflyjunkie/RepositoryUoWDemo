using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.CorpComm;
using System;

namespace AdminPureGold.Repositories.Repositories.CorpComm
{
    public class UnitOfWorkCorpComm : IUnitOfWorkCorpComm, IDisposable
    {
        private bool _disposed = false;
        private readonly CorpCommContext _context;
        private IMcMessageRepository _mcMessageRepository;

        public UnitOfWorkCorpComm(CorpCommContext context)
        {
            _context = context;     
        }

        public IMcMessageRepository McMessageRepository
        {
            get { return _mcMessageRepository ?? (_mcMessageRepository = new McMessageRepository(_context)); }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
