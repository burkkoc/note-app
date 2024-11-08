using NoteApp.Core.Repositories.Interfaces;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Interfaces.Repositories
{
    public interface INoteWriteRepository : IWriteRepository<Note>
    {

    }
}
