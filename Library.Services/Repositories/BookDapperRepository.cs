using Dapper;
using Library.Infrastructure.LibraryDBContext;
using Library.Services.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Library.Services.Repositories
{
    public class BookDapperRepository : IBookDapperRepository
    {
        private readonly DapperContext _context;
        public BookDapperRepository(DapperContext context)
        {
            _context = context;
        }
        Task<object> IBookDapperRepository.AddAsync(object entity)
        {
            throw new NotImplementedException();
        }

        Task IBookDapperRepository.DeleteAsync(object entity)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<object>> IBookDapperRepository.GetAllAsync()
        {
            var query = "select * from ViewBooks";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<object>(query);
                return companies.ToList();
            }
        }

        Task<object> IBookDapperRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IBookDapperRepository.UpdateAsync(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
