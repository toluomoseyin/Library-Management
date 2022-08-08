using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Enums
{
    public enum CustomerStatus
    {
        [Display(Description = "Pending")]
        Pending = 0,

        [Display(Description = "Active")]
        Active = 1,

        [Display(Description = "Disabled")]
        Disabled = 2,
    }

}
