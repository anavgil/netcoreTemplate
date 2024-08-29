using Application.Test.GetById;
using Domain.Interfaces;
using FluentResults;
using System.Collections.ObjectModel;

namespace Application.Test.Service;

public class TestService(IUnitOfWork uow) : ITestService
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

    public Task<IResult<IReadOnlyList<TestQueryDto>>> GetFiltered()
    {
        IQueryRepository<user>
        throw new NotImplementedException();
    }
}
