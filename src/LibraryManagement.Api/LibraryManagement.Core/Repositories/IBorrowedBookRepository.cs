using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Repositories
{
    public interface IBorrowedBookRepository:IRepository<BorrowedBook>
    {
        Task<BorrowedBook> GetCustomerDueToReturnBook(string customerId);

        Task<BorrowedBook> GetBorrowedBookByCustomerId(string customerId);
        Task<BorrowedBook> GetBorrowedBookByBook(string bookId);

        Task<List<BorrowedBook>> GetListByCustomerId(string customerId);
    }
}
