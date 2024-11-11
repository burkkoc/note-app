using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Features.Auth.Login;
using NoteApp.DTOs.Member;
using NoteApp.Interfaces.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemberReadRepository _memberReadRepository;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;

        public LoginHandler(UserManager<IdentityUser> userManager, IJWTService jwtService, IMapper mapper, IMemberReadRepository memberReadRepository)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _memberReadRepository = memberReadRepository;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var identity = await _userManager.FindByEmailAsync(request.Email);
         

            if (identity != null && await _userManager.CheckPasswordAsync(identity, request.Password))
            {
                var member = await _memberReadRepository.GetSingleAsync(m => m.IdentityUserId == identity.Id);
                if (member.Status == Core.Enums.Status.Passive)
                    throw new Exception("The user was deleted.");
                var memberDTO = _mapper.Map<MemberDTO>(member);
                var token = await _jwtService.GenerateTokenAsync(identity);
                return new LoginResponse(token, memberDTO);
            }
            
            throw new UnauthorizedAccessException("Invalid credentials."); 
        }
    }
}
