
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<Customer> GetByEmail(string  email);

        Task<Customer> GetById(string id);

        Task<Customer> GetByLibraryId(Guid libraryId);

        Task<List<Customer>> GetByStatus(CustomerStatus status);

        Task<List<Customer>> GetState(string state);


    }
}
