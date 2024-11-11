using MediatR;
using NoteApp.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Auth.Claims.AssignClaim
{
    public class AssignClaimCommand : IRequest<bool>
    {
        public string Id{ get; set; }
        public string ClaimName { get; set; }
        public AssignClaimCommand(AssignClaimDTO assignClaimDTO)
        {
            Id = assignClaimDTO.Id;
            ClaimName = assignClaimDTO.ClaimName;
        }
    }
}
