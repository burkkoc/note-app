using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Note;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Queries.GetNotesByIdentityId
{
    public class GetNotesByIdentityIdHandler : IRequestHandler<GetNotesByIdentityIdQuery, List<NoteListDTO>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly INoteReadRepository _noteReadRepository;
        public GetNotesByIdentityIdHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, INoteReadRepository noteReadRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _noteReadRepository = noteReadRepository;
        }



        public async Task<List<NoteListDTO>> Handle(GetNotesByIdentityIdQuery request, CancellationToken cancellationToken)
        {
            var identifier = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();
            var user = await _userManager.FindByEmailAsync(identifier);
            var notes = _noteReadRepository.GetWhere(n => n.Member.IdentityUserId.ToString() == user.Id).ToList();
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
