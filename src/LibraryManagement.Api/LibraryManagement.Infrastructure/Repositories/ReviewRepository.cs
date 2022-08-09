using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class ReviewRepository:Repository<Review>,IReviewRepository
    {
        public ReviewRepository(LibraryManagementDbContext libMgtCxt) : base(libMgtCxt)
        {

        }
    }
}
