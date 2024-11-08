using MediatR;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Services;
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
        private readonly JWTService _jwtService;

        public LoginHandler(UserManager<IdentityUser> userManager,  JWTService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = await _jwtService.GenerateTokenAsync(user);
                return new LoginResponse(token);
            }
            
            throw new UnauthorizedAccessException("Invalid credentials."); 
        }
    }
}
