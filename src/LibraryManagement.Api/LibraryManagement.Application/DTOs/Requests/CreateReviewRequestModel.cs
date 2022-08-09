using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class CreateReviewRequestModel
    {
        public string BookId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CustomerId { get; set; }
    }
}
