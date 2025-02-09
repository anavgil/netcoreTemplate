using Application.Items.Dtos;
using FluentResults;
using MediatR;

namespace Application.Items.Get;

public sealed record GetItemsQuery : IRequest<IResult<IReadOnlyCollection<ItemDto>>>
{
}
