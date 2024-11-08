using MediatR;
using NoteApp.DTOs.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommand : IRequest<bool>
    {
        public string Content { get; set; } = null!;
        public string Title { get; set; } = null!;
        public CreateNoteCommand(NoteCreateDTO noteCreateDto)
        {
            Content = noteCreateDto.Content;
            Title = noteCreateDto.Title;
        }
    }

}
