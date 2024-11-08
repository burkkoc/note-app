using NoteApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Repositories.Interfaces
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetWhere(Expression<Func<T, bool>> exp);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> exp);
        Task<T> GetByIdAsync(string Id);
    }
}
