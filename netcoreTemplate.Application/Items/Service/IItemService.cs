using Application.Items.Dtos;
using FluentResults;

namespace Application.Items.Service;

public interface IItemService
{
    Task<IResult<IReadOnlyCollection<ItemDto>>> GetAll();
    Task<IResult<IReadOnlyCollection<ItemDto>>> GetById(Guid id);

    Task<IResult<IReadOnlyList<ItemDto>>> GetFiltered();
}