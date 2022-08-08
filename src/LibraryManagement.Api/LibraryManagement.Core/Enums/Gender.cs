using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Enums
{
    public enum Gender
    {
        [Display(Description = "Female")]
        Female = 1,

        [Display(Description = "Male")]
        Male = 2,
    }
}
