﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Business.Features.Members.Commands.DeleteMember;
using NoteApp.Business.Features.Members.Commands.EditMember;
using NoteApp.Business.Features.Members.Queries.GetAllMembers;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Member;

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
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var member = await _mediator.Send(new GetMemberByIdQuery(Id));
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var members = await _mediator.Send(new GetAllMembersQuery());
                return Ok(members);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve member.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MemberCreateDTO createDTO)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<CreateMemberCommand>(createDTO));
                return Ok("Member created.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to create member.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id) //softdelete
        {
            try
            {
                var result = await _mediator.Send(new DeleteMemberCommand(Id));
                return Ok("Member deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to delete member.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] MemberUpdateDTO updateDTO)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<UpdateMemberCommand>(updateDTO));
                return Ok("Member updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to update member.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }


    }
}
