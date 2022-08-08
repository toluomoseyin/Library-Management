
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(LibraryManagementDbContext libMgtCxt):base(libMgtCxt)
        {

        }
        public async Task<Customer> GetByEmail(string email)
        {
            return await _libraryMgtDbCxt.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Customer> GetById(string id)
        {
            return await _libraryMgtDbCxt.Customers.FindAsync(id);
        }

        public async Task<Customer> GetByLibraryId(Guid libraryId)
        {
            return await _libraryMgtDbCxt.Customers.FirstOrDefaultAsync(x => x.LibraryId == libraryId);
        }

        public Task<List<Customer>> GetByStatus(CustomerStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetState(string state)
        {
            throw new NotImplementedException();
        }
    }
}
