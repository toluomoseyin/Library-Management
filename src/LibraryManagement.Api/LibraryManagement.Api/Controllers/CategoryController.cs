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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BaseResponse>> CreateCategory([FromBody] CreateCategoryRequestModel reqModel)
        {
            BaseResponse result = await _categoryService.CreateCategory(reqModel);
            return Ok(result);
        }


        [HttpGet("[action]", Name = "GetBookById")]
        public async Task<ActionResult<BaseResponse<CategoryViewModel>>> GetById(string categoryId)
        {
            BaseResponse<CategoryViewModel> result = await _categoryService.GetById(categoryId);
            return Ok(result);
        }


        [HttpDelete("[action]")]
        [Authorize(Roles = "admin,labrarian")]
        public async Task<ActionResult<BaseResponse>> DeleteCategory(string categoryId)
        {
            BaseResponse result = await _categoryService.DeleteCategory(categoryId);
            return Ok(result);
        }
    }
}
