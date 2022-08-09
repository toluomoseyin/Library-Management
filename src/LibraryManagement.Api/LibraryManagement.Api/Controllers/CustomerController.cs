using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse<TokenResponseModel>>> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            BaseResponse < TokenResponseModel > result = await _customerService.Login(loginRequestModel);
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse>> AddToRole([FromBody] RoleRequestModel roleRequestModel)
        {
            BaseResponse result = await _customerService.AddToRole(roleRequestModel);
            return Ok(result);
        }

       
        [HttpGet("[action]/{customerId}")]

        public async Task<ActionResult<BaseResponse<CustomerViewModel>>> GetById(string customerId)
        {
            BaseResponse<CustomerViewModel> result = await _customerService.GetById(customerId);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("[action]")]
        public async Task<ActionResult<BaseResponse<CustomerViewModel>>> Update(UpdateCustomerRequestModel req)
        {
            BaseResponse<CustomerViewModel> result = await _customerService.UpdateCustomer(req);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("[action]")]
        public async Task<ActionResult<BaseResponse>> Delete(string customerId)
        {
            BaseResponse result = await _customerService.DeleteCustomer(customerId);
            return Ok(result);
        }
    }
}
