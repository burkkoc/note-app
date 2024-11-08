using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Business.Features.Notes.Commands.CreateNote;
using NoteApp.Business.Features.Notes.Queries.GetAllNotes;
using NoteApp.Business.Features.Notes.Queries.GetNoteById;
using NoteApp.Business.Features.Notes.Queries.GetNotesByIdentityId;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Note;

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public NoteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] NoteCreateDTO createDTO)
        {
            try
            {
                var result = await _mediator.Send(new CreateNoteCommand(createDTO));
                return Ok("User created.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to create note.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetNotesByIdentityId")]
        public async Task<IActionResult> GetNotesByIdentityId()
        {
            try
            {
                var notes = await _mediator.Send(new GetNotesByIdentityIdQuery());
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve notes.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetNoteById")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            try
            {
                var note = await _mediator.Send(new GetNoteByIdQuery(id));
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve note.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetAllNotes")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var notes = await _mediator.Send(new GetAllNotesQuery());
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve notes.", errors = ex.InnerException?.Message ?? ex.Message });
            }
        }



    }
}
