using MediatR;
using Microsoft.AspNetCore.Http;
using NoteApp.Business.Features.Login;
using NoteApp.Business.Features.Members.Commands.DeleteMember;
using NoteApp.Business.Features.Members.Commands.EditMember;
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

            if (request is not LoginCommand && !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("You have to log in first.");
            }

            if (request is DeleteMemberCommand && !user.HasClaim(c => c.Type == CustomClaims.CanDelete && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have claims for deleting.");
            }

            if (request is UpdateMemberCommand && !user.HasClaim(c => c.Type == CustomClaims.CanEdit && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have claims for editing.");
            }

            if (request is CreateMemberCommand && !user.HasClaim(c => c.Type == CustomClaims.CanCreate && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have claims for creating.");
            }

            return await next();
        }
    }

}
