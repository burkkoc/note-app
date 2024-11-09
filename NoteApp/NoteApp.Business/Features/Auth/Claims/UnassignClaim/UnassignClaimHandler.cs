using MediatR;
using Microsoft.AspNetCore.Identity;
using NoteApp.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Auth.Claims.UnassignClaim
{
    public class UnassignClaimHandler : IRequestHandler<UnassignClaimCommand, bool>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UnassignClaimHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(UnassignClaimCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var member = await _userManager.FindByIdAsync(request.Id);
                if (member == null)
                    throw new Exception("Member could not found.");

                var existingClaim = await _userManager.GetClaimsAsync(member);

                foreach (var claim in CustomClaims.CustomClaimList)
                {
                    if (existingClaim.Any(c => c.Type == claim.Type && c.Value == claim.Value) && request.ClaimsList.Contains(claim.Type.ToString()))
                    {
                        await _userManager.RemoveClaimAsync(member, claim);
                    }
                }

                return true;
            }
            catch
            {
                throw new Exception("An error occured.");
            }
        }
    }
}
