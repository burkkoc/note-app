using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Business.Extensions.Mapper;
using NoteApp.Business.Features.Members.Pipelines;
using NoteApp.Business.Services;
using NoteApp.Core.Repositories.Abstracts;
using NoteApp.DataAccess.Contexts;
using NoteApp.DataAccessEFCore.Repositories;
using NoteApp.Interfaces.BusinessServices;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandBehavior<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            AutoMapperConfig.RegisterMappings(services);
            return services;
        }

        
    }
}
