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
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse>> CreateRating([FromBody] CreateRatingRequestModel reqModel)
        {
            BaseResponse result = await _ratingService.CreateRating(reqModel);
            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<BaseResponse<RatingViewModel>>> GetById(string ratingId)
        {
            BaseResponse<RatingViewModel> result = await _ratingService.GetById(ratingId);
            return Ok(result);
        }


        [HttpDelete("[action]")]
        public async Task<ActionResult<BaseResponse>> DeleteRating(string reviewId)
        {
            BaseResponse result = await _ratingService.DeleteRating(reviewId);
            return Ok(result);
        }
    }
}
