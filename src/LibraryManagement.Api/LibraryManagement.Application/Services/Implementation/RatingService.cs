using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Implementation
{
    public class RatingService : IRatingService
    {
        public Task<BaseResponse> CreateRating(CreateRatingRequestModel reqModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> DeleteRating(string reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<RatingViewModel>> GetById(string ratingId)
        {
            throw new NotImplementedException();
        }
    }
}
