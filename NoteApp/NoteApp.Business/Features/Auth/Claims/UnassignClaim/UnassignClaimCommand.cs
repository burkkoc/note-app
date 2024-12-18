﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Auth.Claims.UnassignClaim
{
    public class UnassignClaimCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public List<string> ClaimsList { get; set; } = new();

        public UnassignClaimCommand(string id, List<string> claimsList)
        {
            Id = id;
            ClaimsList.AddRange(claimsList);
        }
    }
}
