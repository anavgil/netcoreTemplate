using Application.Items.Service;
using FluentResults;
using MediatR;

namespace Application.Items.GetById;

public class TestQueryParamRequestRequest(string id) : IRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public string Id { get; private set; } = id;

    public Guid ParsedId => Guid.Parse(Id);
}


public class TestQueryParamRequestHandler(IItemService service) : IRequestHandler<TestQueryParamRequestRequest, IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> Handle(TestQueryParamRequestRequest request, CancellationToken cancellationToken)
    {
        return await service.GetById(request.ParsedId);
    }
}
