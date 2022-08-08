using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class Customer: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public Guid LibraryId { get; set; }
        public string City { get; set; }
        public CustomerStatus CustomerStatus { get; set; }
        public Gender Gender { get; set; }
        public IEnumerable<Rating> Rate { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public  DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
