using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Models;

public class BorrowdBooks
{
    public BorrowdBooks()
    {
        BorrowID = 0;
        MemberID = 0;
        BookID = 0;
        BorrowDate = new DateTime();
        ReturnDate = new DateTime();
        Status = "";
    }

    [Key]
    public int BorrowID { get; set; }
    public int MemberID { get; set; }
    public int BookID { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }

}
