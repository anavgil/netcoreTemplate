using Application.Items.Dtos;
using FluentResults;

namespace Application.Items.Service;

public interface IItemService
{
    Task<IResult<IReadOnlyCollection<ItemResponseDto>>> GetAll();
    Task<IResult<IReadOnlyCollection<ItemResponseDto>>> GetById(Guid id);

    Task<IResult<IReadOnlyList<ItemResponseDto>>> GetFiltered();
}