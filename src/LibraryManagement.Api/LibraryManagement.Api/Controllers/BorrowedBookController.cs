using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BorrowedBookController : ControllerBase
    {
        private readonly IBorrowedBookService _borrowedBookService;

        public BorrowedBookController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse<BorrowBookResponse>>> Borrow([FromBody] BorrowBookRequestModel req)
        {
            BaseResponse<BorrowBookResponse> result = await _borrowedBookService.Borrow(req);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse<ReturnBookResponse>>> Return([FromBody] ReturnBookRequestModel req)
        {
            BaseResponse<ReturnBookResponse> result = await _borrowedBookService.Return(req);
            return Ok(result);
        }


        [HttpGet("{customerId}")]
        public async Task<ActionResult<BaseResponse<List<BorrowBookResponse>>>> GetBorrowedBooksByCustomerId(string customerId)
        {
            BaseResponse<List<BorrowBookResponse>> result = await _borrowedBookService.BorrowedBooks(customerId);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BaseResponse<List<BorrowBookResponse>>>> AllBooks()
        {
            BaseResponse<List<BorrowBookResponse>> result = await _borrowedBookService.GetAll();
            return Ok(result);
        }
    }
}
