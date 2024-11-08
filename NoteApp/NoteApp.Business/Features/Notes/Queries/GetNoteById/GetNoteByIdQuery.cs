using MediatR;
using NoteApp.DTOs.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Queries.GetNoteById
{
    public class GetNoteByIdQuery : IRequest<NoteDTO>
    {
        public Guid Id { get; }
        public GetNoteByIdQuery(Guid noteId)
        {
            Id = noteId;
        }
    }
}
