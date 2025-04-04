using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}
