using netcoreTemplate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreTemplate.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext _dbContext;

        public UnitOfWork(IApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
