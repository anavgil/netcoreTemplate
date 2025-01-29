using Application.Items.GetById;
using FluentResults;

namespace Application.Items.Service;

public interface IItemService
{
    Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAll();
    Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetById(Guid id);

    Task<IResult<IReadOnlyList<TestQueryDto>>> GetFiltered();
}