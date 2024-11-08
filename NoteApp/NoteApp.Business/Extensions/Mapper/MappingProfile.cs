using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Features.Login;
using NoteApp.Business.Features.Members.Commands.EditMember;
using NoteApp.Business.Features.Notes.Commands.CreateNote;
using NoteApp.Business.Features.Users.Commands;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Authentication;
using NoteApp.DTOs.Note;
using NoteApp.DTOs.User;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NoteApp.Business.Extensions.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>();
            CreateMap<GetMemberByIdQuery, MemberDTO>();
            CreateMap<MemberCreateDTO, CreateMemberCommand>();
            CreateMap<MemberUpdateDTO, UpdateMemberCommand>();
            CreateMap<Member, MemberListDTO>();
            CreateMap<UpdateMemberCommand, Member>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateMemberCommand, IdentityUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MemberUpdateDTO, IdentityUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<LoginDTO, LoginCommand>();
            CreateMap<CreateMemberCommand, IdentityUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<CreateMemberCommand, Member>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());




            CreateMap<CreateNoteCommand, Note>();
            CreateMap<Note, NoteListDTO>();
            CreateMap<Member, Note>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Id));

        }
    }
}
