using Microsoft.EntityFrameworkCore;
using netcoreTemplate.Domain.Interfaces;

namespace netcoreTemplate.Infrastructure.Repositories.Base
{
    public class QueryRepository<TEntity, TContext> : IQueryRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly TContext _context;

        public QueryRepository(TContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            DbSet = context.Set<TEntity>();
            _context = context;
        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
             return await DbSet.FindAsync(id);
        }
    }
}
