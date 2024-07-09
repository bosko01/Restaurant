using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> CreateNewAsync(T t);

        Task<T?> GetByIdAsync(Guid id);

        Task DeleteAsync(T t);

        Task<T> UpdateAsync(T t);
    }
}