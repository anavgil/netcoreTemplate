using netcoreTemplate.Domain.Interfaces;

namespace netcoreTemplate.Infrastructure.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
