
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Application.Services.Implementation;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.TestData;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace LibraryManagement.Test
{
    public class BookTest
    {
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();
        private readonly Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
        private readonly Mock<ICacheRepository> _cacheRepository = new Mock<ICacheRepository>();

        private readonly IConfiguration _configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        private readonly BookService _sut;
        public BookTest()
        {
            _sut = new BookService(_configuration, _cacheRepository.Object, _bookRepository.Object, _categoryRepository.Object);
        }
        [Fact]
        public async void Create_book_should_return_true_when_Category_exist()
        {

            //Arrange
            Book book = BookTestData.GetBook();
            Category category=BookTestData.GetCategory();
            CreateBookRequestModel req = BookTestData.ReturnTrueReq();

            _categoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(category);
            _bookRepository.Setup(x => x.AddAsync(It.IsAny<Book>())).ReturnsAsync(book);
            _cacheRepository.Setup(x=>x.GetData<Book>(It.IsAny<string>())).Returns(book);
          
            //Act
            var result = await _sut.CreateBook(req);

            //Assert
            Assert.True(result.Status);
        }


        [Fact]
        public async void Create_book_should_return_false_when_Category_does_not_exist()
        {

            //Arrange
            Book book = BookTestData.GetBook();
            Category category = null;
            CreateBookRequestModel req = BookTestData.ReturnTrueReq();

            _categoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(category);
            _bookRepository.Setup(x => x.AddAsync(It.IsAny<Book>())).ReturnsAsync(book);
            _cacheRepository.Setup(x => x.GetData<Book>(It.IsAny<string>())).Returns(book);

            //Act
            var result = await _sut.CreateBook(req);

            //Assert
            Assert.False(result.Status);
        }
    }
}