using AutoMapper;
using NoteApp.DTOs.User;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Extensions.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>();

        }
    }
}
