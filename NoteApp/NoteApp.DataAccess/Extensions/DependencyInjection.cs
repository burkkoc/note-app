using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DataAccess.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NoteAppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString(NoteAppDbContext.ConnectionString));
            });
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {

                option.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<NoteAppDbContext>().AddDefaultTokenProviders();


            return services;
        }
    }
}
