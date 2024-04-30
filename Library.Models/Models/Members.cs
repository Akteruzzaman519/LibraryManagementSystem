using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Models;

public class Members
{
    public Members()
    {
        MemberID = 0;
        FirstName = "";
        LastName = "";
        Email = "";
        PhoneNumber = "";
        RegistrationDate = new DateTime();

    }
    [Key]
    public int MemberID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }

}
