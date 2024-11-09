using NoteApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public const string CanCreateOwnNote = "CanCreateOwnNote";
        public const string CanDeleteOwnNote = "CanDeleteOwnNote";
        public const string CanEditOwnNote = "CanEditOwnNote";

        public const string CanGivePermission = "CanGivePermission";
        public const string CanRemovePermission = "CanRemovePermission";

        //public static List<string> CustomClaimsList = new List<string> { CanCreateMember, CanEditMember, CanDeleteMember, CanReadAnyMember, CanReadAnyNote, CanCreateOwnNote, CanDeleteOwnNote, CanDeleteAnyNote, CanEditOwnNote, CanGivePermission };

        public static List<Claim> CustomClaimList = new()
        {
            new Claim(CanCreateMember, ClaimStates.Yes.ToString()),
            new Claim(CanEditMember, ClaimStates.Yes.ToString()),
            new Claim(CanDeleteMember, ClaimStates.Yes.ToString()),
            new Claim(CanReadAnyMember, ClaimStates.Yes.ToString()),
            new Claim(CanDeleteOwnNote, ClaimStates.Yes.ToString()),
            new Claim(CanEditOwnNote, ClaimStates.Yes.ToString()),
            new Claim(CanCreateOwnNote, ClaimStates.Yes.ToString()),
            new Claim(CanReadAnyNote, ClaimStates.Yes.ToString()),
            new Claim(CanGivePermission, ClaimStates.Yes.ToString()),
            new Claim(CanRemovePermission, ClaimStates.Yes.ToString())
        };


    }
}
