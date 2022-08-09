using LibraryManagement.Application.Helpers.Implementation;
using LibraryManagement.Application.Helpers.Interface;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Application.Services.Implementation;

namespace LibraryManagement.Api.Extensions
{
    public static  class Services
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBorrowedBookService, BorrowedBookService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOnbaordingService, OnboardingService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
