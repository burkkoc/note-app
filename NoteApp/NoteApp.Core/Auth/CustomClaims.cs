using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Auth
{
    public static class CustomClaims
    {
        public const string CanCreate = "CanCreate";
        public const string CanEdit = "CanEdit";
        public const string CanDelete = "CanDelete";
    }
}
