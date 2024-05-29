using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.IRepositories
{
    public interface IBookDapperRepository
    {
        Task<IEnumerable<Object>> GetAllAsync();
        Task<Object> GetByIdAsync(int id);
        Task<Object> AddAsync(Object entity);
        Task UpdateAsync(Object entity);
        Task DeleteAsync(Object entity);
    }
}
