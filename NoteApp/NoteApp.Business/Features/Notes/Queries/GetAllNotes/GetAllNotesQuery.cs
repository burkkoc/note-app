using MediatR;
using NoteApp.DTOs.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Queries.GetAllNotes
{
    public class GetAllNotesQuery : IRequest<List<NoteListDTO>>
    {
    }
}
