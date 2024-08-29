using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IQueryRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                        List<Expression<Func<T, object>>> includes = null,
                                                        bool disableTracking = true, CancellationToken ct = default);
    }
}
