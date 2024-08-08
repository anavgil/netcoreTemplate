using FluentResults;
using netcoreTemplate.Application.Test.GetById;
using System.Collections.ObjectModel;

namespace netcoreTemplate.Application.Test.Service;

public class TestService : ITestService
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAll()
    {
        return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>().AsReadOnly()));
    }

    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetById(Guid id)
    {
        TestQueryDto item = new(id);

        return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>() { item }));
    }
}
