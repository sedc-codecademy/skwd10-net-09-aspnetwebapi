using Notes.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Notes.Infrastracture
{
    public class PasswordHasher
        : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var data = ASCIIEncoding.ASCII.GetBytes(password);
            var shaProvider = new SHA1CryptoServiceProvider();
            return ASCIIEncoding.ASCII.GetString(shaProvider.ComputeHash(data));
        }

        //public string HashPassword2(string password)
        //{
        //byte[] salt; // random
        //new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        //var provider = new Rfc2898DeriveBytes(password, salt, 100);
        //var hash = provider.GetBytes(20);
        //var result = new byte[36];
        //Array.Copy(salt, 0, result,0, 16);
        //    Array.Copy(salt, 0, result, 16, 20);
        //    return ASCIIEncoding.ASCII.GetString(shaProvider.ComputeHash(result))
            // https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
        //}
    }
}
