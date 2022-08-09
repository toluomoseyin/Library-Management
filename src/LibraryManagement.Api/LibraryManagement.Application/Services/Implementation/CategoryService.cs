using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse> CreateCategory(CreateCategoryRequestModel reqModel)
        {
            await _categoryRepository.AddAsync(new Core.Entities.Category
            {
                Id = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                LastUpdatedTime = DateTime.Now,
                Description = reqModel.Description,
                Name = reqModel.Name,
            });
            return BaseResponse.Success("Successful");
        }

        public Task<BaseResponse> DeleteCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<CategoryViewModel>> GetById(string categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
