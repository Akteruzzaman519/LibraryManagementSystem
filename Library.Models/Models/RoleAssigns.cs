using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Models
{
    public class RoleAssigns
    {
        public RoleAssigns()
        {
            RoleAssignId = 0;
            RoleId = 0;
            UserId = 0;
        }
        [Key]
        public int RoleAssignId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
