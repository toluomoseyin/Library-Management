using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Response
{
    public class ReviewViewModel
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
