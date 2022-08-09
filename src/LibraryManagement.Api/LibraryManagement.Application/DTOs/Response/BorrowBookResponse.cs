using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Response
{
    public class BorrowBookResponse
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string BookId { get; set; }

        public DateTime ReturnDueDate { get; set; }

        public bool HasReturned { get; set; }
    }

    public class ReturnBookResponse
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string BookId { get; set; }

        public DateTime ReturnDueDate { get; set; }

        public bool HasReturned { get; set; }
        public decimal PricePenalty { get; set; }
    }
}
