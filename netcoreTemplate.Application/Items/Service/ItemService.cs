using Application.Items.Dtos;
using Domain.Interfaces;
using FluentResults;
using System.Collections.ObjectModel;

namespace Application.Items.Service;

public class ItemService(IUnitOfWork uow) : IItemService
{
    public async Task<IResult<IReadOnlyCollection<ItemResponseDto>>> GetAll()
    {
        var t = new Collection<ItemResponseDto>
        {
            new(){ Id = Guid.NewGuid() }
        };
        return await Task.FromResult(Result.Ok(t.AsReadOnly()));
    }

    public async Task<IResult<IReadOnlyCollection<ItemResponseDto>>> GetById(Guid id)
    {
        ItemResponseDto item = new()
        {
            Id = id
        };

        return await Task.FromResult(Result.Ok(new Collection<ItemResponseDto>() { item }));
    }

    public Task<IResult<IReadOnlyList<ItemResponseDto>>> GetFiltered()
    {
        throw new NotImplementedException();
    }
}
