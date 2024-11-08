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
        public const string CanReadAnyMember = "CanReadAnyMember";

        public const string CanReadAnyNote = "CanReadAnyNote";
        public const string CanDeleteAnyNote = "CanEditAnyNote";

        public const string CanReadOwnNote = "CanReadNote";
        public const string CanCreateOwnNote = "CanCreateOwnNote";
        public const string CanDeleteOwnNote = "CanDeleteOwnNote";
        public const string CanEditOwnNote = "CanEditOwnNote";

        public const string CanGivePermission = "CanGivePermission";
    }
}
