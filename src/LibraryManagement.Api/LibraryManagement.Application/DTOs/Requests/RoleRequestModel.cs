using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class RoleRequestModel
    {
        [Required]
        public string CustomerId { get; set; }
        public Role Role { get; set; }
    }
}
