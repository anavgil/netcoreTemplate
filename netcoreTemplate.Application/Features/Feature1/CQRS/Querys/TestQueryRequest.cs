using MediatR;
using netcoreTemplate.Application.Features.Feature1.Dtos;
using netcoreTemplate.Application.Features.Feature1.Service;
using System.Collections.ObjectModel;

namespace netcoreTemplate.Application.Features.Feature1.CQRS.Querys;

public class TestQueryRequestRequest : IRequest<IReadOnlyCollection<TestQueryModel>>
{
}

public class TestQueryRequestHandler(IService service) : IRequestHandler<TestQueryRequestRequest, IReadOnlyCollection<TestQueryModel>>
{
    public async Task<IReadOnlyCollection<TestQueryModel>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {

        var f = await service.GetAsync();

        return await Task.FromResult(new Collection<TestQueryModel>());
    }
}