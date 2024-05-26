using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using netcoreTemplate.Application.Features.Feature1.Dtos;
using netcoreTemplate.Domain.Model;

namespace netcoreTemplate.Application.Features.Feature1.Service;

public class Service(IMapper mapper, IUnitOfWork uow) : IService
{
    public async Task<IEnumerable<TestDTO>> GetAsync()
    {
        var repo = uow.Repository<TestModel>();

        var query = repo.MultipleResultQuery()
                    .AndFilter(model => model.Id == 1);

        var f = repo.ToQueryable(query);


        return await mapper.ProjectTo<TestDTO>(f).ToListAsync();
    }
}
