using Application.Items.GetById;
using FluentResults;

namespace Application.Items.Service;

public interface IItemService
{
    Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAllAsync(CancellationToken ct);
    Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetByIdAsync(Guid id, CancellationToken ct);

    Task<IResult<IReadOnlyList<TestQueryDto>>> GetFiltered();
}