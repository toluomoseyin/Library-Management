using LibraryManagement.Application.Helpers.Implementation;
using LibraryManagement.Application.Helpers.Interface;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Api
{
    public static class SeedRolesAndAdmin
    {
        public static async Task CreateRoles(IApplicationBuilder app, IConfiguration configuration)
        {
          
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help
                var userManager = (UserManager<Customer>)scope.ServiceProvider.GetService(typeof(UserManager<Customer>));
                var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
                var context = (LibraryManagementDbContext)scope.ServiceProvider.GetService(typeof(LibraryManagementDbContext));
                var passwordHasher = (PasswordHasher)scope.ServiceProvider.GetService(typeof(IPasswordHasher));
                var customerRepository = (CustomerRepository)scope.ServiceProvider.GetService(typeof(ICustomerRepository));
                context.Database.EnsureCreated();

                string[] roleNames = configuration["Roles"].Split(',');
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                var admin = new Customer
                {

                    UserName = configuration["Admin:Username"],
                    Email = configuration["Admin:Username"],
                    FirstName="Tolu",
                    LastName="Omoseyin",
                    Gender =Gender.Male,
                };
               
                var appUser = await userManager.FindByEmailAsync(configuration["Admin:Username"]);

                if (appUser == null)
                {
                    var createPowerUser = await userManager.CreateAsync(admin, configuration["Admin:Password"]);
                    if (createPowerUser is not null)
                    {

                        await userManager.AddToRoleAsync(admin, "Admin");

                    }
                }
            }
        }
    }
}
