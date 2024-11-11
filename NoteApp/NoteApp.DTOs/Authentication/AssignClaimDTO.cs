using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DTOs.Authentication
{
    public class AssignClaimDTO
    {
        public string Id { get; set; } = null!;
        public string ClaimName { get; set; } = null!;
    }
}
