using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class AuthReturnDto
    {
        public AuthReturnDto()
        {
            UserId = 0;
            UserName = string.Empty;
            Token = string.Empty;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string ?ErrMsg { get; set; }
    }
}
