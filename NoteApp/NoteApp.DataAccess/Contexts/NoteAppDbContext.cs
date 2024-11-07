using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Enums;
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

        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            
        }

    }
}
