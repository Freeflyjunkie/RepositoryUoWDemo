using System;
using AdminPureGold.Repositories.Interfaces.WeichertCore;
using AdminPureGold.Repositories.EF;

namespace AdminPureGold.Repositories.Repositories.WeichertCore
{
    public class UnitOfWorkCore : IUnitOfWorkCore, IDisposable 
    {
        private bool _disposed = false;
        private readonly WeichertCoreContext _context;        
        private IOfficeRepository _officeRepository;
        private IPersonRepository _personRepository;
        private IPersonToRelateRepository _personToRelateRepository;
        private IRelateToEmailRepository _relateToEmailRepository;
        private IRelateToNameRepository _relateToNameRepository;
        private IRelateToPhoneRepository _relateToPhoneRepository;
        private IWeichertOneUserRepository _weichertOneUserRepository;        

        public UnitOfWorkCore(WeichertCoreContext context)
        {
            _context = context;
        }
        
        public IOfficeRepository OfficeRepository
        {
            get { return _officeRepository ?? (_officeRepository = new OfficeRepository(_context)); }
        }

        public IPersonRepository PersonRepository
        {
            get { return _personRepository ?? (_personRepository = new PersonRepository(_context)); }
        }

        public IPersonToRelateRepository PersonToRelateRepository
        {
            get { return _personToRelateRepository ?? (_personToRelateRepository = new PersonToRelateRepository(_context)); }
        }

        public IRelateToEmailRepository RelateToEmailRepository
        {
            get { return _relateToEmailRepository ?? (_relateToEmailRepository = new RelateToEmailRepository(_context)); }
        }

        public IRelateToNameRepository RelateToNameRepository
        {
            get { return _relateToNameRepository ?? (_relateToNameRepository = new RelateToNameRepository(_context)); }
        }

        public IRelateToPhoneRepository RelateToPhoneRepository
        {
            get { return _relateToPhoneRepository ?? (_relateToPhoneRepository = new RelateToPhoneRepository(_context)); }
        }

        public IWeichertOneUserRepository WeichertOneUserRepository
        {
            get { return _weichertOneUserRepository ?? (_weichertOneUserRepository = new WeichertOneUserRepository(_context)); }
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
