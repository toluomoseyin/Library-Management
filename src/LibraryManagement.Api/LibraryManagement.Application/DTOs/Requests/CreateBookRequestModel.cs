using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class CreateBookRequestModel
    {
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
    }
}
