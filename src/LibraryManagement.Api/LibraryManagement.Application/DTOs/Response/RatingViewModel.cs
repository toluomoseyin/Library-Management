using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Response
{
    public class RatingViewModel
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string BookId { get; set; }
        public int Rate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
