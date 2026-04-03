using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DH.EventDrivenAuditSystem.Infrastructure.Repositories
{
    public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return  _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity?> GetByIdAsync(params object?[] keyValues)
        {
            return await _context.Set<TEntity>().FindAsync(keyValues);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

    }
}
