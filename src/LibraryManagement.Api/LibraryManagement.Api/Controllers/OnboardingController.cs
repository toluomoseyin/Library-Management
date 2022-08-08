using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IOnbaordingService _customerService;

        public OnboardingController(IOnbaordingService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse>> OnboardCustomer([FromBody] CreateCustomerReqModel reqModel)
        {
            var result = await _customerService.OnboardCustomer(reqModel);
            return Ok(result);
        }
    }
}
