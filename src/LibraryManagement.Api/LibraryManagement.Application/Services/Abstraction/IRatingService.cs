using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Abstraction
{
    public interface IRatingService
    {
        Task<BaseResponse> CreateRating(CreateRatingRequestModel reqModel);
        Task<BaseResponse> DeleteRating(string reviewId);
        Task<BaseResponse<RatingViewModel>> GetById(string ratingId);
    }
}
