using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class RoleAssignsDto
    {
        public RoleAssignsDto()
        {
            RoleId = 0;
            UserId = 0;
        }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
