using MediatR;
using Microsoft.AspNetCore.Http;
using NoteApp.Business.Features.Auth.Claims.AssignClaim;
using NoteApp.Business.Features.Auth.Claims.UnassignClaim;
using NoteApp.Business.Features.Auth.Login;
using NoteApp.Business.Features.Members.Commands.DeleteMember;
using NoteApp.Business.Features.Members.Commands.EditMember;
using NoteApp.Business.Features.Notes.Commands.CreateNote;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Core.Auth;
using NoteApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Pipelines
{

    public class CommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommandBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext.User;
            if(user.Identity == null)
            {
                throw new Exception("User not found.");
            }

            if (request is not LoginCommand && !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("You have to log in first.");
            }

            if (request is DeleteMemberCommand && !user.HasClaim(c => c.Type == CustomClaims.CanDeleteMember && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to delete members.");
            }

            if (request is UpdateMemberCommand && !user.HasClaim(c => c.Type == CustomClaims.CanEditMember && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to edit members.");
            }

            if (request is CreateMemberCommand && !user.HasClaim(c => c.Type == CustomClaims.CanCreateMember && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to create members.");
            }

            if (request is CreateNoteCommand && !user.HasClaim(c => c.Type == CustomClaims.CanCreateOwnNote && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to create notes.");
            }

            if (request is AssignClaimCommand && !user.HasClaim(c => c.Type == CustomClaims.CanGivePermission && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to assign members.");
            }

            if (request is UnassignClaimCommand && !user.HasClaim(c => c.Type == CustomClaims.CanRemovePermission && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to unassign members.");
            }

            return await next();
        }
    }

}
