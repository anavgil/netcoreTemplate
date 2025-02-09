using Application.Items.Dtos;
using Application.Items.Service;
using FluentResults;
using MediatR;

namespace Application.Items.Get;

public class TestQueryRequestHandler(IItemService service) : IRequestHandler<GetItemsQuery, IResult<IReadOnlyCollection<ItemResponseDto>>>
{

    public async Task<IResult<IReadOnlyCollection<ItemResponseDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await service.GetAll();
    }
}