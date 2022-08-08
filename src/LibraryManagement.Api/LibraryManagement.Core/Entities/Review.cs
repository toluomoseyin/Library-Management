using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class Review:BaseEntity
    {      
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
