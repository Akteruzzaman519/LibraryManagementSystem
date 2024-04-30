using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class BorrowdBooksDto
    {
        public BorrowdBooksDto()
        {
            MemberID = 0;
            BookID = 0;
            ReturnDate = new DateTime();
            Status = "";
        }

        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
