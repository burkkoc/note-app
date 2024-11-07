using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Business.Features.Users.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IMediator _mediator;
        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {
                var query = new GetUserByIdQuery(Id);
                var user = await _mediator.Send(query);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user", error = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string Id)
        {
            try
            {
                var query = new GetUserByIdQuery(Id);
                var user = await _mediator.Send(query);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user", error = ex.Message });
            }
        }


    }
}
