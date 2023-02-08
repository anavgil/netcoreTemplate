using netcoreTemplate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
