namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync(CancellationToken cancellationToken = default);
        void Rollback();

        IQueryRepository<TEntity> GetQueryRepository<TEntity>() where TEntity : class, new();

        ICommandRepository<TEntity> GetCommandRepository<TEntity>() where TEntity : class, new();
    }
}
