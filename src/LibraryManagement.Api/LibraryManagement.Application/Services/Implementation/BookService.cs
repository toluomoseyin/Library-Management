using AutoMapper;
using LibraryManagement.Application.Constants;
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
    public class BookService : IBookService
    {
        private readonly IConfiguration _configuration;
        private readonly ICacheRepository _cacheRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICategoryRepository _categoryRepository;
        public BookService(IConfiguration configuration,
            ICacheRepository cacheRepository,
            IBookRepository bookRepository,
            ICategoryRepository categoryRepository)
        {
            _configuration = configuration;
            _cacheRepository = cacheRepository;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<BaseResponse> CreateBook(CreateBookRequestModel reqModel)
        {
            var category = await _categoryRepository.GetByIdAsync(reqModel.CategoryId);
            if (category == null)
                return BaseResponse.Failure("Category does not exist");
            Book book = new Book
            {
                Id=Guid.NewGuid().ToString(),
                ISBN = reqModel.ISBN,
                Author = reqModel.Author,
                CreateTime = DateTime.Now,
                LastUpdatedTime = DateTime.Now,
                Edition = reqModel.Edition,
                Price = reqModel.Price,
                Publisher = reqModel.Publisher,
                Title = reqModel.Title,
                Language = reqModel.Language,
                AvailableCopies = reqModel.AvailableCopies,
                CategoryId = reqModel.CategoryId,
                Description = reqModel.Description,
                Pages = reqModel.Pages,
                PublishedDate = reqModel.PublishedDate,
               
            };
           var createdBook = await _bookRepository.AddAsync(book);

            if (createdBook is not null)
                _cacheRepository.SetData<Book>($"{CacheConstant.BOOK_ID_CACHE_KEY}{createdBook.Id}",
                                                                       createdBook, DateTimeOffset.Now.AddHours(12));
            return BaseResponse.Success("Book was successfully created");
        }

        public async Task<BaseResponse<BookViewModel>> GetById(string bookId)
        {
            BookViewModel bookViewModel = new BookViewModel();
            var book = _cacheRepository.GetData<Book>
                                          ($"{CacheConstant.BOOK_ID_CACHE_KEY}{bookId}");
            if (book is null)
                book = await _bookRepository.GetByIdAsync(bookId);


            if (book is not null)
                _cacheRepository.SetData<Book>($"{CacheConstant.BOOK_ID_CACHE_KEY}{bookId}",
                                                             book, DateTimeOffset.Now.AddHours(12));
            else
                return BaseResponse<BookViewModel>.Failure(bookViewModel, "Book not found.");

            bookViewModel.Id = book.Id;
            bookViewModel.Title = book.Title;
            bookViewModel.Author = book.Author;
            bookViewModel.Description = book.Description;
            bookViewModel.Author=book.Author;
            bookViewModel.PublishedDate = book.PublishedDate;
            bookViewModel.CategoryId = book.CategoryId;
            bookViewModel.Publisher=book.Publisher;
            bookViewModel.AvailableCopies = book.AvailableCopies;
            bookViewModel.Price = book.Price;

            return BaseResponse<BookViewModel>.Success(bookViewModel, "Operation successful");

        }

        public async Task<BaseResponse<BookViewModel>> UpdateBook(BookUpdate req)
        {
            BookViewModel bookViewModel = new BookViewModel();
            var book = _cacheRepository.GetData<Book>
                                          ($"{CacheConstant.BOOK_ID_CACHE_KEY}{req.Id}");
            if (book is null)
                book = await _bookRepository.GetByIdAsync(req.Id);

            if (book is null)
                return BaseResponse<BookViewModel>.Failure(bookViewModel, "Book was not found");

            var category = await _categoryRepository.GetByIdAsync(req.CategoryId);
            if (category == null)
                return BaseResponse<BookViewModel>.Failure(bookViewModel,"Category does not exist");
            book.Id = req.Id;
            book.Title = req.Title;
            book.Author = req.Author;
            book.Description = req.Description;
            book.Author = req.Author;
            book.PublishedDate = req.PublishedDate;
            book.CategoryId = req.CategoryId;
            book.Publisher = req.Publisher;
            book.AvailableCopies = req.AvailableCopies;
            book.Price = req.Price;

            await _bookRepository.UpdateAsync(book);
            _cacheRepository.RemoveData($"{CacheConstant.BOOK_ID_CACHE_KEY}{req.Id}");
            return await GetById(req.Id);

        }

        public async Task<BaseResponse> DeleteBook(string bookId)
        {
            var book = _cacheRepository.GetData<Book>
                                        ($"{CacheConstant.BOOK_ID_CACHE_KEY}{bookId}");
            if (book is null)
                book = await _bookRepository.GetByIdAsync(bookId);

            await _bookRepository.DeleteAsync(book);

            _cacheRepository.RemoveData($"{CacheConstant.BOOK_ID_CACHE_KEY}{bookId}");

            return BaseResponse.Success("Operation successful");
        }

     
    }
}
