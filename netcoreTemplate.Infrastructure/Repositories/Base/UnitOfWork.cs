using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork, IDisposable
    where TContext : DbContext
{
    private bool disposed = false;
    private readonly Dictionary<Type, object> repositories = [];

    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public ICommandRepository<TEntity> GetCommandRepository<TEntity>() where TEntity : class, new()
    {
        if (repositories.ContainsKey(typeof(TEntity)))
        {
            return (ICommandRepository<TEntity>)repositories[typeof(TEntity)];
        }

        var repository = new CommandRepository<TEntity, TContext>(context);
        repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public IQueryRepository<TEntity> GetQueryRepository<TEntity>() where TEntity : class, new()
    {
        if (repositories.ContainsKey(typeof(TEntity)))
        {
            return (IQueryRepository<TEntity>)repositories[typeof(TEntity)];
        }

        var repository = new QueryRepository<TEntity, TContext>(context);
        repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }
}
