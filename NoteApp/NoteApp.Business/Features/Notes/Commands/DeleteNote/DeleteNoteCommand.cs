using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommand : IRequest<bool>
    {
        public  Guid Id{ get; set; }
        public DeleteNoteCommand(Guid id)
        {

            Id = id;

        }
    }
}
