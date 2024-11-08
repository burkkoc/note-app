using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Business.Features.Members.Commands.DeleteMember;
using NoteApp.Business.Features.Members.Commands.EditMember;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public MemberController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {
                var query = new GetMemberByIdQuery(Id);
                var user = await _mediator.Send(query);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user: ", error = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] MemberCreateDTO createDTO)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<CreateMemberCommand>(createDTO));
                return Ok("User created.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to create user: ", error = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id) //softdelete
        {
            try
            {
                var result = await _mediator.Send(new DeleteMemberCommand { MemberId = Id});
                return Ok("User deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to delete user: ", error = ex.Message });
            }
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromForm] MemberUpdateDTO updateDTO)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<UpdateMemberCommand>(updateDTO));
                return Ok("User updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to update user: ", error = ex.Message });
            }
        }


    }
}
