using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Commands.CreateNote
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, bool>
    {
        private readonly INoteWriteRepository _noteWriteRepository;
        private readonly IMemberReadRepository _memberReadRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateNoteHandler(INoteWriteRepository noteWriteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMemberReadRepository memberReadRepository, UserManager<IdentityUser> userManager)
        {
            _noteWriteRepository = noteWriteRepository;
            _memberReadRepository = memberReadRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var memberMail = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (memberMail == null)
                throw new ArgumentNullException("The member coul NOT found.");
            var identityUser = await _userManager.FindByEmailAsync(memberMail);

            var member = await _memberReadRepository.GetSingleAsync(m => m.IdentityUserId == identityUser.Id);

            var note = _mapper.Map<Note>(request);
            _mapper.Map(member, note);

            await _noteWriteRepository.AddAsync(note);
            await _noteWriteRepository.SaveAsync();
            return true;

        }
    }
}
