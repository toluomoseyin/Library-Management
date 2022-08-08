using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Abstraction
{
    public interface ICustomerService
    {
        Task<BaseResponse<TokenResponseModel>> Login(LoginRequestModel loginRequestModel);
        Task<BaseResponse> AddToRole(RoleRequestModel roleRequestModel);
        Task<BaseResponse<CustomerViewModel>> GetById(string customerId);
        Task<BaseResponse<CustomerViewModel>> UpdateCustomer(UpdateCustomerRequestModel req);
        Task<BaseResponse> DeleteCustomer(string customerId);
    }
}
