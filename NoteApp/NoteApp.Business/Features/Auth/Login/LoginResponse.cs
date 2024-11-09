using NoteApp.DTOs.Member;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Login
{
    public class LoginResponse
    {
        public MemberDTO MemberDTO { get; set; }
        public string Token { get; set; }
        public LoginResponse(string token, MemberDTO memberDTO)
        {
            Token = token;
            MemberDTO = memberDTO;
        }
    }
}
