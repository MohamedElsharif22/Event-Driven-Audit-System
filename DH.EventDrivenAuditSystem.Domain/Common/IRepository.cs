using System.Linq.Expressions;

namespace DH.EventDrivenAuditSystem.Domain.Common
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<TEntity?> GetByIdAsync(int id);
        public IQueryable<TEntity> GetAllAsync();
        public Task AddAsync(TEntity entity);
        public void Delete(TEntity entity);
        public void Update(TEntity entity);
    }
}
