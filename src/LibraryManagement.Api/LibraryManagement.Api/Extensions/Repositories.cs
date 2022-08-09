using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Repositories;

namespace LibraryManagement.Api.Extensions
{
    public static class Repositories
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
