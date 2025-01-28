using Application.Items.GetById;
using Domain.Interfaces;
using FluentResults;
using System.Collections.ObjectModel;

namespace Application.Items.Service;

public class ItemService(IUnitOfWork uow) : IItemService
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAllAsync(CancellationToken ct)
    {
        var t = new Collection<TestQueryDto>
        {
            new(Guid.NewGuid())
        };
        //return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>().AsReadOnly()));
        return await Task.FromResult(Result.Ok(t.AsReadOnly()));
    }

    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        TestQueryDto item = new(id);

        return await Task.FromResult(Result.Fail<IReadOnlyCollection<TestQueryDto>>("error").v);

        //return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>() { item }));
    }

    public Task<IResult<IReadOnlyList<TestQueryDto>>> GetFiltered()
    {
        throw new NotImplementedException();
    }
}
