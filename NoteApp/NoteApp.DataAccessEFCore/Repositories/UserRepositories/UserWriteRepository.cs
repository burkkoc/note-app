using NoteApp.DataAccess.Contexts;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DataAccessEFCore.Repositories.UserRepositories
{
    public class UserWriteRepository : WriteRepository<Member>
    {
        public UserWriteRepository(NoteAppDbContext context) : base(context)
        {

        }
    }
}
