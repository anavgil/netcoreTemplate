namespace Domain.Interfaces
{
    public interface IQueryRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
    }
}
