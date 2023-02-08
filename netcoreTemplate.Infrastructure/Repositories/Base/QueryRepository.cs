using netcoreTemplate.Domain.Interfaces;

namespace netcoreTemplate.Infrastructure.Repositories.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
