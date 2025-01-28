using Application.Test.GetById;
using Domain.Interfaces;
using FluentResults;
using System.Collections.ObjectModel;

namespace Application.Test.Service;

public class ItemService(IUnitOfWork uow) : IItemService
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAll()
    {
        var t = new Collection<TestQueryDto>
        {
            new(Guid.NewGuid())
        };
        return await Task.FromResult(Result.Ok(t.AsReadOnly()));
    }

    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetById(Guid id)
    {
        TestQueryDto item = new(id);

        return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>() { item }));
    }

    public Task<IResult<IReadOnlyList<TestQueryDto>>> GetFiltered()
    {
        throw new NotImplementedException();
    }
}
