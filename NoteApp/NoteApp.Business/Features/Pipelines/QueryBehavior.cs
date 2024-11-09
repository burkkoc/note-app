using MediatR;
using Microsoft.AspNetCore.Http;
using NoteApp.Business.Features.Auth.Login;
using NoteApp.Business.Features.Members.Commands.DeleteMember;
using NoteApp.Business.Features.Members.Commands.EditMember;
using NoteApp.Business.Features.Members.Queries.GetAllMembers;
using NoteApp.Business.Features.Notes.Commands.CreateNote;
using NoteApp.Business.Features.Notes.Queries.GetAllNotes;
using NoteApp.Business.Features.Notes.Queries.GetNotesByIdentityId;
using NoteApp.Business.Features.Users.Queries;
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

    public class QueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryBehavior(IHttpContextAccessor httpContextAccessor)
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

            if((request is GetMemberByIdQuery || request is GetAllMembersQuery) && !user.HasClaim(c=>c.Type == CustomClaims.CanReadAnyMember && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to read members.");
            }

            if (request is GetAllNotesQuery && !user.HasClaim(c => c.Type == CustomClaims.CanReadAnyNote && c.Value == ClaimStates.Yes.ToString()))
            {
                throw new UnauthorizedAccessException("You do NOT have permission to read all notes.");
            }


            return await next();
        }
    }

}
