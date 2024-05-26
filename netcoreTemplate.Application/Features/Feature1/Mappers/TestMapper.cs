using AutoMapper;
using netcoreTemplate.Application.Features.Feature1.Dtos;
using netcoreTemplate.Domain.Model;

namespace netcoreTemplate.Application.Features.Feature1.Mappers;

public class TestMapper : Profile
{
    public TestMapper()
    {
        CreateProjection<TestDTO, TestModel>();
    }
}
