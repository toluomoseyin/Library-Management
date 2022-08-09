

using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;

namespace LibraryManagement.Application.Services.Abstraction
{
    public interface IBookService
    {
        Task<BaseResponse> CreateBook(CreateBookRequestModel reqModel);
        Task<BaseResponse<BookViewModel>> GetById(string bookId);
        Task<BaseResponse<BookViewModel>> UpdateBook(BookUpdate req);
        Task<BaseResponse> DeleteBook(string bookId);
       
    }
}
