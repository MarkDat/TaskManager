using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TM.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Entities { get; }
        DbContext DbContext { get; }
        Task<IList<T>> GetAllAsync();
        T Find(params object[] keyValues);
        Task<T> FindAsync(params object[] keyValues);
        Task InsertAsync(T entity, bool saveChanges = true);
        Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        Task DeleteAsync(int id, bool saveChanges = true);
        Task DeleteAsync(T entity, bool saveChanges = true);
        Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
