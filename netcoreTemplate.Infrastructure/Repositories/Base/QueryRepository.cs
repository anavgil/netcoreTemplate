using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Base;

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
        return await DbSet.AsNoTracking()
                            .ToListAsync()
                            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                                List<Expression<Func<TEntity, object>>> includes = null, 
                                                bool disableTracking = true, CancellationToken ct = default)
    {
        IQueryable<TEntity> query = DbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            query = query.AsSplitQuery();
        }

        if (filter != null) query = query.Where(filter);

        if (orderBy != null)
            return await orderBy(query)
                        .ToListAsync(ct)
                        .ConfigureAwait(false);

        return await query
                    .ToListAsync(ct)
                    .ConfigureAwait(false);
    }

    public async Task<TEntity> GetByIdAsync(long id)
    {
        return await DbSet.FindAsync(id).ConfigureAwait(false);
    }


}
