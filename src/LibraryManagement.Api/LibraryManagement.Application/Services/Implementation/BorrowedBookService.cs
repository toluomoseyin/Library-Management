using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Implementation
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;

        public BorrowedBookService(IBorrowedBookRepository borrowedBookRepository,
            IBookRepository bookRepository,
            ICustomerRepository customerRepository,
            IConfiguration configuration)
        {
            _borrowedBookRepository = borrowedBookRepository;
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        public async Task<BaseResponse<BorrowBookResponse>> Borrow(BorrowBookRequestModel req)
        {
            var borrowedBook= new BorrowBookResponse();
           var book = await _bookRepository.GetByIdAsync(req.BookId);
            if (book == null)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "Book was not found");

            if (book.AvailableCopies == 0)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "This book is not available");

            var customer = await _customerRepository.GetByIdAsync(req.CustomerId);
            if (customer == null)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "Customer was not found");

           var dueToReturnBook = await  _borrowedBookRepository.GetCustomerDueToReturnBook(req.CustomerId, _configuration.GetValue<int>("ReturnDuration"));

            if (dueToReturnBook is not null)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "You have a book due for return");

             var bookToAdd = new BorrowedBook
             {
                BookId = req.BookId,
                CustomerId = req.CustomerId,
                HasReturned = false,
                ReturnDueDate = DateTime.Now.AddDays(_configuration.GetValue<int>("ReturnDuration")),
                CreateTime=DateTime.Now,
                LastUpdatedTime=DateTime.Now,
            };

            var addedBook = await _borrowedBookRepository.AddAsync(bookToAdd);
            borrowedBook.ReturnDueDate = bookToAdd.ReturnDueDate;
            borrowedBook.HasReturned = addedBook.HasReturned;
            borrowedBook.CustomerId = req.CustomerId;
            borrowedBook.BookId = req.BookId;
            return BaseResponse<BorrowBookResponse>.Success(borrowedBook, "Operation successful");

        }
    }
}
