using AutoMapper;
using MediatR;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Note;
using NoteApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Queries.GetNotesByIdentityId
{
    public class GetNotesByIdentityIdQuery : IRequest<List<NoteListDTO>>
    {
        
    }
}
