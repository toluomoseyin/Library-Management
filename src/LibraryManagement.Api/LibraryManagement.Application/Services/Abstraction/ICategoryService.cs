using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<BaseResponse> CreateCategory(CreateCategoryRequestModel reqModel);
        Task<BaseResponse> DeleteCategory(string categoryId);
        Task<BaseResponse<CategoryViewModel>> GetById(string categoryId);
    }
}
