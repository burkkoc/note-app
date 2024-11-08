using NoteApp.Core.Repositories.Abstracts;
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
    public class MemberWriteRepository : WriteRepository<Member>, IMemberWriteRepository
    {
        public MemberWriteRepository(NoteAppDbContext context) : base(context)
        {

        }
    }
}
