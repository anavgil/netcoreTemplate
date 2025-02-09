using Application.Items.Dtos;
using FluentResults;
using MediatR;

namespace Application.Items.GetById;

public sealed record GetItemByIdQuery(Guid Id) :IRequest<IResult<IReadOnlyCollection<ItemDto>>> { }