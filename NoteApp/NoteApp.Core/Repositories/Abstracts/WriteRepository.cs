using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NoteApp.Core.Entities.Interfaces;
using NoteApp.Core.Repositories.Interfaces;

namespace NoteApp.Core.Repositories.Abstracts
{
    public abstract class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly IdentityDbContext<IdentityUser, IdentityRole, string> _context;
        protected readonly DbSet<T> _table;

        protected WriteRepository(IdentityDbContext<IdentityUser, IdentityRole, string> context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            model.CreatedDate = DateTime.Now;
            model.Status = Enums.Status.Added;
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
            model.Status = Enums.Status.Passive;
            model.DeletedDate = DateTime.Now;
            model.DeletedBy = username;
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Deleted;

        }
        public async Task<bool> SoftRemoveAsync(string id, string username)
        {
            T model = await Table.FindAsync(Guid.Parse(id));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (model.Status == Enums.Status.Passive)
                throw new Exception($"Already deleted.");

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
            model.Status = Enums.Status.Modified;
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
