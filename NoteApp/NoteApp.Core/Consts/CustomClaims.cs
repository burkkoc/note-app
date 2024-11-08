using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Auth
{
    public static class CustomClaims
    {
        public const string CanCreateMember = "CanCreateMember";
        public const string CanEditMember = "CanEditMember";
        public const string CanDeleteMember = "CanDeleteMember";
        public const string CanReadNote = "CanReadNote";
    }
}
