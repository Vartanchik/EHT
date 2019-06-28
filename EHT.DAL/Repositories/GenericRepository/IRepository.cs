using EHT.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace EHT.DAL.Repositories.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task CreateOrUpdate(TEntity entity);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task DeleteAsync(int id);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> AsQueryable();
    }
}
