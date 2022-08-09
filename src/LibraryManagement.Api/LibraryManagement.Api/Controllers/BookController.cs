using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse>> CreateBook([FromBody] CreateBookRequestModel reqModel)
        {
            BaseResponse result = await _bookService.CreateBook(reqModel);
            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<BaseResponse<BookViewModel>>> GetById(string bookId)
        {
            BaseResponse<BookViewModel> result = await _bookService.GetById(bookId);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BaseResponse<BookViewModel>>> UpdateBook(BookUpdate req)
        {
            BaseResponse<BookViewModel> result = await _bookService.UpdateBook(req);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<BaseResponse>> DeleteBook(string bookId)
        {
            BaseResponse result = await _bookService.DeleteBook(bookId);
            return Ok(result);
        }   


    }
}
