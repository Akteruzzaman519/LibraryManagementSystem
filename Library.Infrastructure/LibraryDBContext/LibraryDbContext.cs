using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.LibraryDBContext
{
    public class LibraryDbContext:DbContext
    {
        public LibraryDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public  DbSet<Authors> Authors { get; set; }
        public  DbSet<Books> Books { get; set; }
        public  DbSet<BorrowdBooks> BorrowdBooks { get; set; }
        public  DbSet<Members> Members { get; set; }
        public  DbSet<Users> Users { get; set; }
        public  DbSet<Roles> Roles { get; set; }
        public  DbSet<RoleAssigns> RoleAssigns { get; set; }

    }
}
