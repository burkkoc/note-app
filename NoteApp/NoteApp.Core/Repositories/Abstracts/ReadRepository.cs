using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Entities.Base;
using NoteApp.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NoteApp.Core.Repositories.Interfaces;
using NoteApp.Core.Enums;

namespace NoteApp.Core.Repositories.Abstracts
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        protected readonly IdentityDbContext<IdentityUser, IdentityRole, string> _context;
        protected readonly DbSet<T> _table;
        

        public ReadRepository(IdentityDbContext<IdentityUser, IdentityRole, string> context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;


        public async Task<T> GetByIdAsync(string id) => await Table.FindAsync(Guid.Parse(id));


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> exp) => await Table.FirstOrDefaultAsync(exp);


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> exp) => Table.Where(exp);
        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true)
        {
            return await GetAllActives(tracking).ToListAsync();
        }
        protected IQueryable<T> GetAllActives(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Status.Passive);

            return tracking ? values : values.AsNoTracking();
        }

    }
}
