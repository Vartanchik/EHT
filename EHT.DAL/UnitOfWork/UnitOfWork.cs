using EHT.DAL.Entities;
using EHT.DAL.Entities.AppUser;
using EHT.DAL.Repositories.ConcreteRepositories.AppUserRepository;
using EHT.DAL.Repositories.GenericRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace EHT.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private IAppUserRepository<AppUser> _appUsers;
        private IRepository<Organization> _organizations;
        private IRepository<Country> _countries;
        private IRepository<Business> _businesses;
        private IRepository<Family> _families;
        private IRepository<Offering> _offerings;
        private IRepository<Department> _departments;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IAppUserRepository<AppUser> AppUsers => _appUsers ?? (_appUsers = new AppUserRepository(_context, _userManager));
        public IRepository<Organization> Organizations => _organizations ?? (_organizations = new RepositoryBase<Organization>(_context));
        public IRepository<Country> Countries => _countries ?? (_countries = new RepositoryBase<Country>(_context));
        public IRepository<Business> Businesses => _businesses ?? (_businesses = new RepositoryBase<Business>(_context));
        public IRepository<Family> Families => _families ?? (_families = new RepositoryBase<Family>(_context));
        public IRepository<Offering> Offerings => _offerings ?? (_offerings = new RepositoryBase<Offering>(_context));
        public IRepository<Department> Departments => _departments ?? (_departments = new RepositoryBase<Department>(_context));
        
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
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
