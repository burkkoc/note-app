using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Auth.Claims.AssignClaim
{
    public class AssignClaimHandler : IRequestHandler<AssignClaimCommand, bool>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemberReadRepository _memberReadRepository;
        public AssignClaimHandler(UserManager<IdentityUser> userManager, IMemberReadRepository memberReadRepository)
        {
            _userManager = userManager;
            _memberReadRepository = memberReadRepository;
        }
        public async Task<bool> Handle(AssignClaimCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var member = await _memberReadRepository.GetByIdAsync(request.Id);
                var identityUser = await _userManager.FindByIdAsync(member.IdentityUserId);
                if (identityUser == null)
                    throw new Exception("Member could not found.");

                var existingClaim = await _userManager.GetClaimsAsync(identityUser);

                foreach (var claim in CustomClaims.CustomClaimList)
                {
                    if (!existingClaim.Any(c => c.Type == claim.Type && c.Value == claim.Value) && request.ClaimName == claim.Type.ToString())
                    {
                        await _userManager.AddClaimAsync(identityUser, claim);
                    }
                }
                if (request.ClaimName == CustomClaims.CanDeleteMember || request.ClaimName == CustomClaims.CanCreateMember || request.ClaimName == CustomClaims.CanEditMember)
                    await _userManager.AddClaimAsync(identityUser, CustomClaims.CustomClaimList.FirstOrDefault(c => c.Type == CustomClaims.CanReadAnyMember) ?? throw new ArgumentNullException());

                if (request.ClaimName == CustomClaims.CanDeleteAnyNote)
                    await _userManager.AddClaimAsync(identityUser, CustomClaims.CustomClaimList.FirstOrDefault(c => c.Type == CustomClaims.CanReadAnyNote) ?? throw new ArgumentNullException());

                return true;
            }
            catch
            {
                throw new Exception("An error occured.");
            }

        }
    }
}
