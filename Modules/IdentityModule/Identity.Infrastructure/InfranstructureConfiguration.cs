using Common.Infrastructure;
using Common.Infrastructure.DependencyInjection;
using Identity.Application;
using Identity.Presentation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public static class InfranstructureConfiguration
    {
        public static Assembly[] ModuleAssList = {typeof(ApplicationConfiguration).Assembly
                ,typeof(InfranstructureConfiguration).Assembly,typeof(AssemblyReference).Assembly };

        public static IServiceCollection AddInfranstructureConfiguration(this  IServiceCollection services)
        {
            services.AddDependyConfiguration([typeof(InfranstructureConfiguration).Assembly]);
           // services.AddMediatRConfiguration()
            return services;
        }
    }
}
