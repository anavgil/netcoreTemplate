using FluentValidation;
using netcoreTemplate.application.CQRS.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreTemplate.Application.CQRS.Querys
{
    public class TestQueryParamRequestValidator: AbstractValidator<TestQueryParamRequestRequest>
    {
        public TestQueryParamRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
