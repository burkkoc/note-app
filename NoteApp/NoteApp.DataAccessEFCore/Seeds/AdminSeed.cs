using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Core.Auth;
using NoteApp.Core.Enums;
using NoteApp.DataAccess.Contexts;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
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



           await AddAdmin(userManager, serviceProvider);
            
            await Task.CompletedTask;
        }

        private static async Task AddAdmin(UserManager<IdentityUser> userManager, IServiceProvider serviceProvider)
        {
            var writeMemberRepository = serviceProvider.GetRequiredService<IMemberWriteRepository>();
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
            if (result.Succeeded )
            {

                await userManager.AddToRoleAsync(adminUser, Enum.GetName(typeof(Roles), 1) ?? throw new NullReferenceException());
                await AddClaims(userManager, adminUser);
            }
            await AddAsMember(writeMemberRepository, adminUser);
        }
        private static async Task AddAsMember(IMemberWriteRepository writeRepository, IdentityUser identityUser)
        {
            Member adminMember = new()
            {
                Status = Status.Added,
                Email = AdminEmail,
                FirstName = AdminEmail.ToUpper(),
                LastName = AdminEmail.ToUpper(),
                Gender = true,
                IdentityUser = identityUser,

                CreatedBy = "System"
            };
            await writeRepository.AddAsync(adminMember);
      await      writeRepository.SaveAsync();
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
                new Claim(CustomClaims.CanReadAnyMember, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanReadOwnNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanDeleteOwnNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanEditOwnNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanCreateOwnNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanReadAnyNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanGivePermission, ClaimStates.Yes.ToString()),
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
