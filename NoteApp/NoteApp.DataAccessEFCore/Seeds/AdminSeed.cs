using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Core.Enums;
using NoteApp.DataAccess.Contexts;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DataAccessEFCore.Seeds
{
    internal static class AdminSeed
    {
        private const string AdminEmail = "admin@noteapp.com";
        private const string AdminPassword = "Asd123$.";

        public static async Task SeedAsync(IServiceProvider serviceProvider, NoteAppDbContext context)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync(Enum.GetName(typeof(Roles), 1) ?? throw new NullReferenceException()))
            {
                await AddRoles(roleManager);
            }
            
           await AddAdmin(userManager, context);
            
            await Task.CompletedTask;
        }

        private static async Task AddAdmin(UserManager<IdentityUser> userManager, NoteAppDbContext context)
        {
            var adminUser = await userManager.FindByEmailAsync(AdminEmail);
            if(adminUser != null) { return; }

            adminUser = new()
            {
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail.ToUpper(),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true
            };

            //adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, AdminPassword);
            var result = await userManager.CreateAsync(adminUser, AdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Enum.GetName(typeof(Roles), 1) ?? throw new NullReferenceException());
            }
            //var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString())!.Id;

            //await context.Member.AddAsync(member);

            //await context.SaveChangesAsync();
        }

        private static async Task AddRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = Enum.GetNames(typeof(Roles));
            for (int i = 0; i < roles.Length; i++)
            {
                if (await roleManager.Roles.AnyAsync(role => role.Name == roles[i]))
                {
                    continue;
                }
                var role = new IdentityRole(roles[i])
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                await roleManager.CreateAsync(role);
            }
        }
    }
}
