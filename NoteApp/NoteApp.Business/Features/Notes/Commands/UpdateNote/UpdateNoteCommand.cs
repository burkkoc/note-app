using MediatR;
using NoteApp.DTOs.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public string Title { get; set; } = null!;

        public UpdateNoteCommand(NoteUpdateDTO updateDTO)
        {
            Content = updateDTO.Content;
            Id = updateDTO.Id;
            Title = updateDTO.Title;
        }
    }
}
