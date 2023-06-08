using netcoreTemplate.Domain.Interfaces;

namespace netcoreTemplate.Infrastructure.Repositories.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Task.FromResult((IReadOnlyList<T>)Enumerable.Empty<T>());
            //throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await Task.FromResult<T>(null);
            //throw new NotImplementedException();
        }
    }
}
