using Application.Items.Dtos;
using Application.Items.Service;
using FluentResults;
using MediatR;

namespace Application.Items.GetById;

public class GetItemByIdQueryHandler(IItemService service) : IRequestHandler<GetItemByIdQuery, IResult<IReadOnlyCollection<ItemResponseDto>>>
{
    public async Task<IResult<IReadOnlyCollection<ItemResponseDto>>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetById(request.Id);
    }
}
