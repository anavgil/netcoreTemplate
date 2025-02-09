using Application.Items.Dtos;
using Application.Items.Service;
using FluentResults;
using MediatR;

namespace Application.Items.Get;

public class TestQueryRequestHandler(IItemService service) : IRequestHandler<GetItemsQuery, IResult<IReadOnlyCollection<ItemDto>>>
{

    public async Task<IResult<IReadOnlyCollection<ItemDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await service.GetAll();
    }
}