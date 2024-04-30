using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class UsersDto
    {
        public UsersDto()
        {
            UserName = "";
            Password = "";
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
