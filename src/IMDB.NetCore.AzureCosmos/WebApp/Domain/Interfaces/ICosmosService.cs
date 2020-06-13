using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Domain.Interfaces
{
    public interface ICosmosService<T>
    {
        Task<IEnumerable<T>> GetQueryAsync(string query);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T item);
        Task UpdateAsync(string id, T item);
        Task DeleteAsync(string id);
    }
}
