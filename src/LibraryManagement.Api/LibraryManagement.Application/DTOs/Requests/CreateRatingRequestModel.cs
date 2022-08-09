using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class CreateRatingRequestModel
    {
        public string CustomerId { get; set; }
        public string BookId { get; set; }
        public int Rate { get; set; }
    }
}
