using LibraryManagement.Application.Constants;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Response;
using LibraryManagement.Application.Helpers.Interface;
using LibraryManagement.Application.Services.Abstraction;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Application.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        public CustomerService(ICacheRepository cacheRepository,
            ICustomerRepository customerRepository,
            IPasswordHasher passwordHasher,
            IConfiguration configuration,
            UserManager<Customer> userManager, 
            SignInManager<Customer> signInManager)
        {
            _cacheRepository = cacheRepository;
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<BaseResponse<TokenResponseModel>> Login(LoginRequestModel loginRequestModel)
        {
            TokenResponseModel loginResponse = new TokenResponseModel();
              var  customer = await _userManager.FindByEmailAsync(loginRequestModel.Email);
            var result = await _signInManager.PasswordSignInAsync(loginRequestModel.Email,
                          loginRequestModel.Password,false, lockoutOnFailure: true);

            if (!result.Succeeded)
                return BaseResponse<TokenResponseModel>.Failure(loginResponse, "Username or password is invalid.");
            if (customer is not null)
                _cacheRepository.SetData<Customer>($"{CacheConstant.CUSTOMER_EMAIL_CACHE_KEY}{loginRequestModel.Email}",
                                                                       customer, DateTimeOffset.Now.AddHours(12));
            else
                return BaseResponse<TokenResponseModel>.Failure(loginResponse, "Username or password is invalid.");

            if (customer.CustomerStatus == CustomerStatus.Disabled)
                return BaseResponse<TokenResponseModel>.Failure(loginResponse, "This customer is not active");

            IList<string> customerRoles = await _userManager.GetRolesAsync(customer);

            IList<Claim> claims = CreateClaims(customer, customerRoles.First());

            var jwtModel = GenerateJwtToken(claims);

            return BaseResponse<TokenResponseModel>.Success(jwtModel);


        }


        private List<Claim> CreateClaims(Customer customer, string role)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("email", customer.Email));
            claims.Add(new Claim("id", customer.Id));
            claims.Add(new Claim("name", customer.FirstName));
            claims.Add(new Claim("stamp", customer.SecurityStamp));
            claims.Add(new Claim("role", role));
            return claims;

        }

        private TokenResponseModel GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var tokenTimeout = _configuration.GetValue<double>("JWTSettings:TokenTimeOut");
            var jwt = CreateSerialized(claims, TimeSpan.FromMinutes(tokenTimeout));

            return new TokenResponseModel
            {
                Access_token = jwt,
                Expires_in = ((tokenTimeout * 60) - 1).ToString(),
                Token_type = "Bearer"
            };
        }

        public string CreateSerialized(IEnumerable<Claim> claims, TimeSpan expiresIn)
        {
            var jwtValidIssuer = _configuration.GetValue<string>("JWT:ValidIssuer");
            var jwtAudienceId = _configuration.GetValue<string>("JWT:AudienceId");
            var jwtSecret = _configuration.GetValue<string>("JWT:Secret");

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var expires = DateTimeOffset.Now.AddMinutes(expiresIn.TotalMinutes);

            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(jwtValidIssuer, jwtAudienceId, claims, null, expires.UtcDateTime, signingCredentials);
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        public async Task<BaseResponse> AddToRole(RoleRequestModel roleRequestModel)
        {
            var customer = _cacheRepository.GetData<Customer>
                                          ($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{roleRequestModel.CustomerId}");
            if(customer is null)
                customer = await _customerRepository.GetByIdAsync(roleRequestModel.CustomerId);
           
            if (customer is null)
                return BaseResponse.Failure("Customer was not found");
            else
                _cacheRepository.SetData<Customer>($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{roleRequestModel.CustomerId}",
                                                                      customer, DateTimeOffset.Now.AddHours(12));
            var result = await _userManager.AddToRoleAsync(customer, roleRequestModel.Role.ToString());

            if (result is not null && result.Succeeded)
                return BaseResponse.Success($"Customer has been added to {roleRequestModel.Role.ToString()}");
            string message = "Errors  - ";
            int i = 0;
            IEnumerable<IdentityError> errors = result.Errors;
            foreach (var error in errors)
            {
                i++;
                message += $"{i}.   {error.Description}  ";
            }
            return BaseResponse.Failure(message);
        }

        public async Task<BaseResponse<CustomerViewModel>> GetById(string customerId)
        {
            CustomerViewModel customerViewModel= new CustomerViewModel();
            var customer = _cacheRepository.GetData<Customer>
                                         ($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{customerId}");
            if (customer is null)
                customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer is null)
                return BaseResponse<CustomerViewModel>.Failure(customerViewModel, "Customer was not found");
            else
                _cacheRepository.SetData<Customer>($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{customerId}",
                                                                      customer, DateTimeOffset.Now.AddHours(12));
            customerViewModel.Id= customerId;
            customerViewModel.State=customer.State;
            customerViewModel.Email=customer.Email;
            customerViewModel.City=customer.City;
            customerViewModel.LibraryId=customer.LibraryId;
            customerViewModel.CustomerStatus=customer.CustomerStatus;
            customerViewModel.FirstName=customer.FirstName;
            customerViewModel.LastName=customer.LastName;

            return BaseResponse<CustomerViewModel>.Success(customerViewModel, "Operation successful");
        }

        public async Task<BaseResponse<CustomerViewModel>> UpdateCustomer(UpdateCustomerRequestModel req)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            var customer = _cacheRepository.GetData<Customer>
                                          ($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{req.Id}");
            if (customer is null)
                customer = await _customerRepository.GetByIdAsync(req.Id);

            if (customer is null)
                return BaseResponse<CustomerViewModel>.Failure(customerViewModel, "Customer was not found");

            customer.State= req.State;
            customer.Email= req.Email;
            customer.City= req.City;
            customer.UserName = req.Email;
            customer.PhoneNumber = req.PhoneNumber;
            customer.FirstName=req.FirstName;
            customer.LastName=req.LastName;
            customer.Gender=req.Gender;
            customer.UpdatedAt=DateTime.Now;

            await _customerRepository.UpdateAsync(customer);
            _cacheRepository.RemoveData($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{req.Id}");
            return await GetById(req.Id);
                     
        }

        public async Task<BaseResponse> DeleteCustomer(string customerId)
        {
            var customer = _cacheRepository.GetData<Customer>
                                        ($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{customerId}");
            if (customer is null)
                customer = await _customerRepository.GetByIdAsync(customerId);

            await _customerRepository.DeleteAsync(customer);

            _cacheRepository.RemoveData($"{CacheConstant.CUSTOMER_ID_CACHE_KEY}{customerId}");

            return BaseResponse.Success("Operation successful");
        }
    }
}
