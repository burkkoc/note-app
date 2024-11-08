using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Business.Features.Login;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Business.Services;
using NoteApp.DTOs.Authentication;

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
                return BadRequest(new { message = "Log in failed: ", error = ex.Message });
            }
        }
    }
}
