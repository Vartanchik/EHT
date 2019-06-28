using EHT.DAL.Entities;
using EHT.DAL.Repositories.GenericRepository;
using System;
using System.Threading.Tasks;

namespace EHT.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Organization> Organizations { get; }
        IRepository<Country> Countries { get; }
        IRepository<Business> Businesses { get; }
        IRepository<Family> Families { get; }
        IRepository<Offering> Offerings { get; }
        IRepository<Department> Departments { get; }

        void Commit();

        Task CommitAsync();

    }
}
