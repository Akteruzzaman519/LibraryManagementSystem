using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class AuthorsDto
    {
        public AuthorsDto()
        {
            AuthorName = "";
            AuthorBio = "";
        }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
    }
}
