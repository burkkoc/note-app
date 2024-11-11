using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Business.Features.Auth.Claims.AssignClaim;
using NoteApp.Business.Features.Auth.Claims.UnassignClaim;
using NoteApp.Business.Features.Auth.Login;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Business.Services;
using NoteApp.Core.Auth;
using NoteApp.DTOs.Authentication;
using System.Security.Claims;

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<LoginCommand>(loginDTO));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Log in failed.", error = ex.Message });
            }
        }

        [HttpPost("AssignMember")]
        public async Task<IActionResult> AssignMember(string id, List<string> claims) //front
        {
            try
            {
                //List<string> claims = new(); //silinecek
                var result = await _mediator.Send(new AssignClaimCommand(id, claims));
                return Ok("Assign operation is success.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Assign failed.", error = ex.Message });
            }
        }

        [HttpPost("UnassignMember")]
        public async Task<IActionResult> UnassignMember(string id/*, List<string> claims*/) //front
        {
            try
            {
                List<string> claims = new(); //silinecek
                claims.Add(CustomClaims.CanCreateMember);
                var result = await _mediator.Send(new UnassignClaimCommand(id, claims));
                return Ok("Unassign operation is success.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unassign failed.", error = ex.Message });
            }
        }
    }
}
