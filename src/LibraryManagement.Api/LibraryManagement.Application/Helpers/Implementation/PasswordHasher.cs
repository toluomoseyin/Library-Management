using LibraryManagement.Application.Helpers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Helpers.Implementation
{
    public class PasswordHasher : IPasswordHasher
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20;
        public const int Pbkdf2Iterations = 10000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 0;
        public const int Pbkdf2Index = 1;
        public string HashPassword(string password)
        {

            byte[] salt = new byte[SaltByteSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public bool IsPasswordValid(string password, string correctHash)
        {
            throw new NotImplementedException();
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes_HMACSHA512(password, salt, iterations);

            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
