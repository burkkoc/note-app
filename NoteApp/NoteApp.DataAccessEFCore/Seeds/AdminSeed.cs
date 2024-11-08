using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Core.Auth;
using NoteApp.Core.Enums;
using NoteApp.DataAccess.Contexts;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

            var result = await userManager.CreateAsync(adminUser, AdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Enum.GetName(typeof(Roles), 1) ?? throw new NullReferenceException());
                await AddClaims(userManager, adminUser);
            }
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

        private static async Task AddClaims(UserManager<IdentityUser> userManager, IdentityUser adminUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, Roles.Admin.ToString()),
                new Claim(CustomClaims.CanCreateMember, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanEditMember, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanDeleteMember, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanReadNote, ClaimStates.Yes.ToString()),
            };

            foreach (var claim in claims)
            {
                var existingClaim = await userManager.GetClaimsAsync(adminUser);
                if (!existingClaim.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await userManager.AddClaimAsync(adminUser, claim);
                }
            }
        }
    }
}
