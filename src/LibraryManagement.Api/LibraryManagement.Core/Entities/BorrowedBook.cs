using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class BorrowedBook:BaseEntity
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }

        public DateTime ReturnDueDate { get; set; }

        public bool HasReturned { get; set; }
    }
}
