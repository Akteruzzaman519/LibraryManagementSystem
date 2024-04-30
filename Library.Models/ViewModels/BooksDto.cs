using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class BooksDto
    {
        public BooksDto()
        {
            Title = "";
            ISBN = "";
            AuthorID = 0;
            PublishedDate = new DateTime();
            AvailableCopies = 0;
            TotalCopies = 0;
        }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int AuthorID { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }
}
