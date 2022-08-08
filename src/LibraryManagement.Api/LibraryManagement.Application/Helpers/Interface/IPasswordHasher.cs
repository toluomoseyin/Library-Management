using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Helpers.Interface
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool IsPasswordValid(string password, string correctHash);
    }
}
