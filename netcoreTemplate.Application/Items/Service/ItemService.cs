using Application.Items.Dtos;
using Domain.Interfaces;
using FluentResults;
using System.Collections.ObjectModel;

namespace Application.Items.Service;

public class ItemService(IUnitOfWork uow) : IItemService
{
    public async Task<IResult<IReadOnlyCollection<ItemDto>>> GetAll()
    {
        var t = new Collection<ItemDto>
        {
            new(){ Id = Guid.NewGuid() }
        };
        return await Task.FromResult(Result.Ok(t.AsReadOnly()));
    }

    public async Task<IResult<IReadOnlyCollection<ItemDto>>> GetById(Guid id)
    {
        ItemDto item = new()
        {
            Id = id
        };

        return await Task.FromResult(Result.Ok(new Collection<ItemDto>() { item }));
    }

    public Task<IResult<IReadOnlyList<ItemDto>>> GetFiltered()
    {
        throw new NotImplementedException();
    }
}
