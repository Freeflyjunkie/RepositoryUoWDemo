using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertSL;
using System;

namespace AdminPureGold.Repositories.Repositories.WeichertSL
{
    public class UnitOfWorkSalesListing : IUnitOfWorkSalesListing, IDisposable
    {
        private bool _disposed = false;
        private readonly WeichertSLContext _context;
        private ISaleRepository _saleRepository;
        private IListRepository _listRepository;

        public UnitOfWorkSalesListing(WeichertSLContext context)
        {
            _context = context;
        }

        public IListRepository ListRepository
        {
            get { return _listRepository ?? (_listRepository = new ListRepository(_context)); }
        }

        public ISaleRepository SaleRepository
        {
            get { return _saleRepository ?? (_saleRepository = new SaleRepository(_context)); }
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
