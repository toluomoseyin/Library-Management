using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Response
{
    public class BookViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        //public string PhotoUrl { get; set; }
        public int AvailableCopies { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
    }
}
