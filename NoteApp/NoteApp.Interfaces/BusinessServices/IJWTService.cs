using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Interfaces.BusinessServices
{
    public interface IJWTService
    {
        Task<string> GenerateTokenAsync(IdentityUser identityUser);
    }
}
