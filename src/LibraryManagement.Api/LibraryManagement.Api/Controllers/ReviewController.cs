using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse>> CreateReview([FromBody] CreateReviewRequestModel reqModel)
        {
            BaseResponse result = await _reviewService.CreateReview(reqModel);
            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<BaseResponse<RatingViewModel>>> GetById(string reviewId)
        {
            BaseResponse<RatingViewModel> result = await _reviewService.GetById(reviewId);
            return Ok(result);
        }


        [HttpDelete("[action]")]
        public async Task<ActionResult<BaseResponse>> DeleteReview(string reviewId)
        {
            BaseResponse result = await _reviewService.DeleteReview(reviewId);
            return Ok(result);
        }
    }
}
