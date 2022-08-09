using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Application.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IConfiguration _configuration;
        private readonly ICacheRepository _cacheRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReviewService(IReviewRepository reviewRepository,
            ICustomerRepository customerRepository,
            IBookRepository bookRepository, IConfiguration configuration)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
            _configuration = configuration;
        }

        public async Task<BaseResponse> CreateReview(CreateReviewRequestModel reqModel)
        {
           var book= await _bookRepository.GetByIdAsync(reqModel.BookId);
            if (book is null)
                return BaseResponse.Failure("Book not found");
            var customer = await _customerRepository.GetByIdAsync(reqModel.CustomerId);
            if (customer is null)
                return BaseResponse.Failure("Customer not found");
            var review = new Review()
            {
                Subject = reqModel.Subject,
                Body = reqModel.Body,
                BookId = book.Id,
                CustomerId = customer.Id,
                Id = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                LastUpdatedTime = DateTime.Now,
            };
             return BaseResponse.Success("Operation successful");
        }

        public async Task<BaseResponse> DeleteReview(string reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review is null)
                return BaseResponse.Failure("Review not found");

            await _reviewRepository.DeleteAsync(review);
            return BaseResponse.Success("Operation successful");
        }

        public Task<BaseResponse<RatingViewModel>> GetById(string reviewId)
        {
            throw new NotImplementedException();
        }
    }
}
