using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;

public class CommandRepository<TEntity, TContext> : ICommandRepository<TEntity>
    where TEntity : class, new()
    where TContext : DbContext
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly TContext _context;

    public CommandRepository(TContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        DbSet = context.Set<TEntity>();
        _context = context;
    }

    public Task<TEntity> AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
