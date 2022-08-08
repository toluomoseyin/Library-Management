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
        public async Task<ActionResult<BaseResponse<TokenResponseModel>>> Borrow([FromBody] BorrowBookRequestModel req)
        {
            BaseResponse<BorrowBookResponse> result = await _borrowedBookService.Borrow(req);
            return Ok(result);
        }
    }
}
