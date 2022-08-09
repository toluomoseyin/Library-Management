using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class BorrowBookRequestModel
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string BookId { get; set; }
    }

    public class ReturnBookRequestModel
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string BookId { get; set; }
    }
}
