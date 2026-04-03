using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Infrastructure.Data;
using DH.EventDrivenAuditSystem.Infrastructure.Repositories;
using System.Collections.Concurrent;


namespace DH.EventDrivenAuditSystem.Infrastructure
{
    public class UnitOfWork(AppDbContext context, IServiceProvider serviceProvider) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        //Returns a generic repository for the specified entity type.
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(IRepository<TEntity>);

            return (IRepository<TEntity>)_repositories.GetOrAdd(
                type,
                _ => new Repository<TEntity>(_context)
            );
        }

        ////Returns a specific repository implementation for the specified entity type and repository type.
        //public TRepo Repository<TEntity, TRepo>()
        //    where TRepo : IRepository<TEntity>
        //    where TEntity : BaseEntity
        //{
        //    var type = typeof(TRepo);

        //    return (TRepo)_repositories.GetOrAdd(
        //        type,
        //        _ => _serviceProvider.GetRequiredService<TRepo>()
        //    );
        //}

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public ValueTask DisposeAsync()
            => _context.DisposeAsync();
    }
}
