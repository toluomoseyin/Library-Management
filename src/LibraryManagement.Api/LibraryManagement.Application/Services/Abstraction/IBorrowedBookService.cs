using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Abstraction
{
    public interface IBorrowedBookService
    {
        Task<BaseResponse<BorrowBookResponse>> Borrow(BorrowBookRequestModel req);
        Task<BaseResponse<ReturnBookResponse>> Return(ReturnBookRequestModel req);
        Task<BaseResponse<List<BorrowBookResponse>>> BorrowedBooks(string customerId);
        Task<BaseResponse<List<BorrowBookResponse>>> GetAll();
    }
}
