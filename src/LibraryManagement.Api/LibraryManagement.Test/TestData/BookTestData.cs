using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Test.TestData
{
    public static class BookTestData
    {
        public static Book GetBook()
        {
            return new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Author = "",
                ISBN = "",
            };
        }


        public static Category GetCategory()
        {
            return new Category()
            {
                Id = Guid.NewGuid().ToString(),
               Name="Politics",
               Description="All about politics"
            };
        }

        public static CreateBookRequestModel ReturnTrueReq()
        {
            return new CreateBookRequestModel()
            {
               CategoryId= Guid.NewGuid().ToString(),
               ISBN="",
               Price=100,
            };
        }
    }
}
