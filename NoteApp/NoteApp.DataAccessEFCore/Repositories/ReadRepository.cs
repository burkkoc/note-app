using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Entities.Base;
using NoteApp.DataAccess.Contexts;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DataAccessEFCore.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly NoteAppDbContext _context;

        public ReadRepository(NoteAppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public async Task<T> GetByIdAsync(string id) => await Table.FindAsync(Guid.Parse(id));


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> exp) => await Table.FirstOrDefaultAsync(exp);


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> exp) => Table.Where(exp);
    }
}
