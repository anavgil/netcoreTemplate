using netcoreTemplate.Application.Features.Feature1.Dtos;

namespace netcoreTemplate.Application.Features.Feature1.Service;

public interface IService
{
    Task<IEnumerable<TestDTO>> GetAsync();
}
