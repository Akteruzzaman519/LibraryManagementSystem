using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class RolesDto
    {
        public RolesDto()
        {
            RoleName = string.Empty;
            RoleActiveStatus = true;
        }
        public string RoleName { get; set; }
        public bool RoleActiveStatus { get; set; }
    }
}
