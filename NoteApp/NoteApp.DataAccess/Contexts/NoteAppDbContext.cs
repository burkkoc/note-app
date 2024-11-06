using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DataAccess.Contexts
{
    public class NoteAppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public const string ConnectionString = "NoteApp";
        public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options) : base(options)
        {
        }

        public new virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

       

    }
}
