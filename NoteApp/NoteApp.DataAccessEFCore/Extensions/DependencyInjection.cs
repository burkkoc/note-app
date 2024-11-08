using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Core.Repositories.Interfaces;
using NoteApp.DataAccess.Contexts;
using NoteApp.DataAccessEFCore.Repositories;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.DataAccessEFCore.Seeds;
using NoteApp.Entities.DbSets;


//using NoteApp.DataAccessEFCore.Seeds;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DataAccessEFCore.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMemberReadRepository, MemberReadRepository>();
            services.AddScoped<IMemberWriteRepository, MemberWriteRepository>();
            services.AddScoped<INoteReadRepository, NoteReadRepository>();
            services.AddScoped<INoteWriteRepository, NoteWriteRepository>();

            return services;
        }

        public static async Task Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<NoteAppDbContext>();

                await context.Database.MigrateAsync();

                await AdminSeed.SeedAsync(services, context);
            }
        }
    }
}
