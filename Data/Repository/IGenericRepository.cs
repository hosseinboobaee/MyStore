using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities.Common;

namespace Data.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();
        Task<IReadOnlyList<TEntity>> GetAllasync();
        Task<TEntity> GetEntityById(long entityId);

        Task AddEntity(TEntity entity);

        void UpdateEntity(TEntity entity);

        void RemoveEntity(TEntity entity);

        Task RemoveEntity(long entityId);

        Task SaveChanges();
    }
}