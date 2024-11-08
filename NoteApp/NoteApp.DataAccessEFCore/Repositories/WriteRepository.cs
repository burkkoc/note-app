using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Entities.Base;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp.DataAccess.Contexts;

namespace NoteApp.DataAccessEFCore.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly private NoteAppDbContext _context;

        public WriteRepository(NoteAppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            model.CreatedDate = DateTime.Now;
            model.Status = Core.Enums.Status.Added;
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FindAsync(Guid.Parse(id));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            return Remove(model);

        }

        public bool SoftRemove(T model, string username)
        {
            model.Status = Core.Enums.Status.Passive;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = username;
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Deleted;

        }
        public async Task<bool> SoftRemoveAsync(string id, string username)
        {
            T model = await Table.FindAsync(Guid.Parse(id));
            if(model == null)
                throw new ArgumentNullException(nameof(model));
            
            return SoftRemove(model, username);

        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();


        public bool Update(T model, string username)
        {
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = username;
            model.Status = Core.Enums.Status.Modified;
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> UpdateAsync(T model, string username)
        {
            EntityEntry<T> entityEntry = await Task.FromResult(_context.Update(model));
            if (entityEntry == null)
                throw new ArgumentNullException(nameof(model));
            return Update(model, username);
        }

    }
}
