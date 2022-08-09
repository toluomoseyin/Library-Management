using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Helpers.Interface;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services.Implementation
{
    public class OnboardingService : IOnbaordingService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;
        public OnboardingService(ICustomerRepository customerRepository,
            IPasswordHasher passwordHasher,
            RoleManager<IdentityRole> roleManager,
            UserManager<Customer> userManager,
            IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<BaseResponse> OnboardCustomer(CreateCustomerReqModel req)
        {
            IdentityResult identityResult = new IdentityResult();
            var userCheck = await _customerRepository.GetByEmail(req.Email);

            if (userCheck is not null)
                return BaseResponse.Failure("Sorry, someone else has signed up with this email.");
            
            string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(.+){8,}$";
            bool isPasswordValid = Regex.IsMatch(req.Password, passwordRegex);
            if (!isPasswordValid)            
                return BaseResponse.Failure("Invalid password, Minimum password Length is 8 " +
                                           "and must contain at least 1 number and a special character ");

          
            var customer = new Customer
            {
                UserName = req.Email,
                Email = req.Email,
                State = req.State,
                City = req.City,
                CustomerStatus = CustomerStatus.Active,
                FirstName = req.FirstName,
                LastName = req.LastName,
                LibraryId = Guid.NewGuid(),
                Gender = req.Gender,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
               PhoneNumber=req.PhoneNumber,
            };
            
            var customerIdentityResult = await _userManager.CreateAsync(customer,req.Password);

            if(customerIdentityResult is not null && customerIdentityResult.Succeeded)
                identityResult = await _userManager.AddToRoleAsync(customer, _configuration["DefaultRole"]);

            if(identityResult is not null && identityResult.Succeeded)
                return BaseResponse.Success("Customer was successfully created");
            
            return BaseResponse.Failure("An error ocurred while trying to assign the customer to a role");
                   
        }
    }
}
