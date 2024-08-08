using FluentResults;
using netcoreTemplate.Application.Test.GetById;

namespace netcoreTemplate.Application.Test.Service;

public interface ITestService
{
    Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAll();
    Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetById(Guid id);
}
