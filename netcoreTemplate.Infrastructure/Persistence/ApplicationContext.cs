using Microsoft.EntityFrameworkCore;
using netcoreTemplate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreTemplate.Infrastructure.Persistence;

    public class ApplicationContext:DbContext,IApplicationContext
    {
    }

