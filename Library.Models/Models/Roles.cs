using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Models
{
    public class Roles
    {
        public Roles()
        {
            RoleId = 0;
            RoleName = string.Empty;
            RoleActiveStatus = true;
        }
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleActiveStatus { get; set; }
    }
}
