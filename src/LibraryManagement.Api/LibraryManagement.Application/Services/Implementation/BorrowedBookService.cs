using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.Extensions.Configuration;

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
            var borrowedBook = new BorrowBookResponse();
            var book = await _bookRepository.GetByIdAsync(req.BookId);
            if (book == null)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "Book was not found");

            if (book.AvailableCopies == 0)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "This book is not available");

            var customer = await _customerRepository.GetByIdAsync(req.CustomerId);
            if (customer == null)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "Customer was not found");

            var dueToReturnBook = await _borrowedBookRepository.GetCustomerDueToReturnBook(req.CustomerId);
            var numofDays = _configuration.GetValue<int>("ReturnDuration");
            if (dueToReturnBook is not null && (DateTime.Now - dueToReturnBook.ReturnDueDate).TotalDays >= numofDays)
                return BaseResponse<BorrowBookResponse>.Failure(borrowedBook, "You have a book due for return");

            var bookToAdd = new BorrowedBook
            {
                Id=Guid.NewGuid().ToString(),
                BookId = req.BookId,
                CustomerId = req.CustomerId,
                HasReturned = false,
                ReturnDueDate = DateTime.Now.AddDays(_configuration.GetValue<int>("ReturnDuration")),
                CreateTime = DateTime.Now,
                LastUpdatedTime = DateTime.Now,
            };

            var addedBook = await _borrowedBookRepository.AddAsync(bookToAdd);
            borrowedBook.Id=addedBook.Id;
            borrowedBook.ReturnDueDate = bookToAdd.ReturnDueDate;
            borrowedBook.HasReturned = addedBook.HasReturned;
            borrowedBook.CustomerId = req.CustomerId;
            borrowedBook.BookId = req.BookId;
            return BaseResponse<BorrowBookResponse>.Success(borrowedBook, "Operation successful");

        }

        public async Task<BaseResponse<ReturnBookResponse>> Return(ReturnBookRequestModel req)
        {
            ReturnBookResponse bookResponse = new ReturnBookResponse();
            var book = await _bookRepository.GetByIdAsync(req.BookId);
            if (book is null)
                return BaseResponse<ReturnBookResponse>.Failure(bookResponse, "Book was not found");

            var customer = await _customerRepository.GetByIdAsync(req.CustomerId);
            if (customer is null)
                return BaseResponse<ReturnBookResponse>.Failure(bookResponse, "Customer was not found");


            var boBook = await _borrowedBookRepository.GetBorrowedBookByBook(req.BookId);
            if (!boBook.HasReturned)
                return BaseResponse<ReturnBookResponse>.Failure(bookResponse, "This book has not been returned");

            var borrowedBook = await _borrowedBookRepository.GetBorrowedBookByCustomerId(customer.Id);

            if (borrowedBook is null)
                return BaseResponse<ReturnBookResponse>.Failure(bookResponse, "You have not borrowed a book");

            bookResponse.ReturnDueDate = borrowedBook.ReturnDueDate;
            bookResponse.Id = borrowedBook.Id;         
            bookResponse.CustomerId = customer.Id;
            bookResponse.BookId = book.Id;

            if (borrowedBook.ReturnDueDate <= DateTime.Now)
            {
                borrowedBook.HasReturned = true;
                borrowedBook.LastUpdatedTime = DateTime.Now;

                await _borrowedBookRepository.UpdateAsync(borrowedBook);
                return BaseResponse<ReturnBookResponse>.Success(bookResponse, "Operation successful");
            }

            var daysOwed = GetDaysOwed(borrowedBook.ReturnDueDate);

            var amountToPay = daysOwed * _configuration.GetValue<decimal>("PricePenalty");
            bookResponse.PricePenalty = amountToPay;
            bookResponse.HasReturned = false;
            return BaseResponse<ReturnBookResponse>.Failure(bookResponse, $"You are owing #{amountToPay} for not returning on time");
        }

        public int GetDaysOwed(DateTime returnDate)
        {
            var weekend = new List<DayOfWeek> { DayOfWeek.Sunday, DayOfWeek.Saturday };
            var todayDate = DateTime.Now;

            var totalDays = (todayDate - returnDate).Days;
            int noOfDays = 0;

            for (int x = 0; x <= totalDays; x++)
            {
                DayOfWeek weekday = returnDate.AddDays(x).DayOfWeek;
                if (!weekend.Contains(weekday))
                    noOfDays += 1;
            }
            return noOfDays;
        }

        public async Task<BaseResponse<List<BorrowBookResponse>>> GetAll()
        {
            var borrowedBooksResponse = new List<BorrowBookResponse>();
            var borrowedBooks = await _borrowedBookRepository.GetAllAsync();
            foreach (var book in borrowedBooks)
            {
                borrowedBooksResponse.Add(new BorrowBookResponse
                {
                    BookId = book.BookId,
                    CustomerId = book.CustomerId,
                    HasReturned = book.HasReturned,
                    Id = book.Id,
                    ReturnDueDate = book.ReturnDueDate,
                });
            }
            return BaseResponse<List<BorrowBookResponse>>.Success(borrowedBooksResponse,"Operation successful");
            
            
        }


        public async Task<BaseResponse<List<BorrowBookResponse>>> BorrowedBooks(string customerId)
        {
            var borrowedBooksResponse = new List<BorrowBookResponse>();
            var borrowedBooks = await _borrowedBookRepository.GetListByCustomerId(customerId);
            foreach (var book in borrowedBooks)
            {
                borrowedBooksResponse.Add(new BorrowBookResponse
                {
                    BookId = book.BookId,
                    CustomerId = book.CustomerId,
                    HasReturned = book.HasReturned,
                    Id = book.Id,
                    ReturnDueDate = book.ReturnDueDate,
                });
            }
            return BaseResponse<List<BorrowBookResponse>>.Success(borrowedBooksResponse, "Operation successful");


        }
    }
}
