using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Features.Notes.Queries.GetNoteById;
using NoteApp.DTOs.Note;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Queries.GetAllNotes
{
    public class GetAllNotesHandler : IRequestHandler<GetAllNotesQuery, List<NoteListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly INoteReadRepository _noteReadRepository;
        public GetAllNotesHandler(INoteReadRepository noteReadRepository, IMapper mapper)
        {
            _noteReadRepository = noteReadRepository;
            _mapper = mapper;
        }
        public async Task<List<NoteListDTO>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            var notes = await _noteReadRepository.GetAllAsync();
            List<NoteListDTO> noteList = new();
            foreach (var note in notes)
            {
                var notListeDTO = _mapper.Map<NoteListDTO>(note);
                noteList.Add(notListeDTO);
            }

            return noteList;
        }
    }
}
