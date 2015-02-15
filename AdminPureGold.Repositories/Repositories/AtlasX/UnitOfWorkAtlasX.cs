using System;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.AtlasX;

namespace AdminPureGold.Repositories.Repositories.AtlasX
{

    public class UnitOfWorkAtlasX : IUnitOfWorkAtlasX, IDisposable
    {
        private bool _disposed = false;
        private readonly AtlasXContext _context;
        private IPropertyRepository _propertyRepository;
        private IPropertyAlternateRepository _propertyAlternateRepository;
        private IWAtlasXRepository _wAtlasXRepository;
        private IWAtlasXToAppRepository _wAtlasXToAppRepository;
        private IWAtlasXToAppWPersonRepository _wAtlasXToAppWPersonRepository;

        public UnitOfWorkAtlasX(AtlasXContext context)
        {
            _context = context;
        }

        public IWAtlasXRepository WAtlasXRepository
        {
            get { return _wAtlasXRepository ?? (_wAtlasXRepository = new WAtlasXRepository(_context)); }
        }

        public IWAtlasXToAppRepository WAtlasXToAppRepository
        {
            get { return _wAtlasXToAppRepository ?? (_wAtlasXToAppRepository = new WAtlasXToAppRepository(_context)); }
        }

        public IWAtlasXToAppWPersonRepository WAtlasXToAppWPersonRepository
        {
            get { return _wAtlasXToAppWPersonRepository ?? (_wAtlasXToAppWPersonRepository = new WAtlasXToAppWPersonRepository(_context)); }
        }

        public IPropertyRepository PropertyRepository
        {
            get { return _propertyRepository ?? (_propertyRepository = new PropertyRepository(_context)); }
        }

        public IPropertyAlternateRepository PropertyAlternateRepository
        {
            get
            {
                return _propertyAlternateRepository ??
                       (_propertyAlternateRepository = new PropertyAlternateRepository(_context));
            }
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
