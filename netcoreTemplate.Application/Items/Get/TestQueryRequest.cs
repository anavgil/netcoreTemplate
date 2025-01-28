using Application.Items.GetById;
using Application.Items.Service;
using FluentResults;
using MediatR;

namespace Application.Items.Get;

public class TestQueryRequestRequest : IRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
{
}

public class TestQueryRequestHandler(IItemService service) : IRequestHandler<TestQueryRequestRequest, IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {
        return await service.GetAllAsync(cancellationToken);
    }
}