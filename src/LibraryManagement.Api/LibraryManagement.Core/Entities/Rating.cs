using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class Rating:BaseEntity
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string BookId { get; set; }
        public int Rate { get; set; }
        public Book Book { get; set; }
    }
}
