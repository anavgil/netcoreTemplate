﻿using FluentResults;
using netcoreTemplate.Application.Test.GetById;
using netcoreTemplate.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace netcoreTemplate.Application.Test.Service;

public class TestService(IUnitOfWork uow) : ITestService
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetAll()
    {
        return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>().AsReadOnly()));
    }

    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> GetById(Guid id)
    {
        TestQueryDto item = new(id);

        return await Task.FromResult(Result.Ok(new Collection<TestQueryDto>() { item }));
    }
}
