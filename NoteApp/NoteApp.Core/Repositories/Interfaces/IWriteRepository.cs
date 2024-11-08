using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Repositories.Interfaces
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        bool Remove(T model);
        Task<bool> RemoveAsync(string id);

        bool RemoveRange(List<T> models);
        bool Update(T model, string username);
        Task<bool> UpdateAsync(T model, string username);
        bool SoftRemove(T model, string username);
        Task<bool> SoftRemoveAsync(string id, string username);

        Task<int> SaveAsync();
    }
}

