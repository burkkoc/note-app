using AutoMapper;
using MediatR;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Note;
using NoteApp.DTOs.User;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Queries.GetNoteById
{
    public class GetNoteByIdHandler : IRequestHandler<GetNoteByIdQuery, NoteDTO>
    {
        private readonly INoteReadRepository _noteReadRepository;
        private readonly IMapper _mapper;
        public GetNoteByIdHandler(INoteReadRepository noteReadRepository, IMapper mapper)
        {
            _noteReadRepository = noteReadRepository;
            _mapper = mapper;
        }
        public async Task<NoteDTO> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _noteReadRepository.GetByIdAsync(request.Id.ToString());
            if (note == null) throw new Exception("Note not found.");
            return _mapper.Map<NoteDTO>(note);
        }
    }
}
