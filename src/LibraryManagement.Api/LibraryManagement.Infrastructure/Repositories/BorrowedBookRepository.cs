using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BorrowedBookRepository:Repository<BorrowedBook>,IBorrowedBookRepository
    {
        public BorrowedBookRepository(LibraryManagementDbContext libMgtCxt) : base(libMgtCxt)
        {

        }

        public async Task<BorrowedBook> GetCustomerDueToReturnBook(string customerId, int numofDays)
        {
            return await _libraryMgtDbCxt.BorrowedBooks
                                                       .Where(x => x.CustomerId == customerId
                                                       && !x.HasReturned && (DateTime.Now - x.ReturnDueDate).TotalDays >= numofDays)
                                                       .FirstOrDefaultAsync();
        }
    }
}
