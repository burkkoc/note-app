using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Services
{
    public class PasswordService
    {
        public string GenerateRandomPassword()
        {
            var random = new Random();
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%*_-+=<>?";

            char[] password =
            [
                uppercase[random.Next(uppercase.Length)],
                uppercase[random.Next(uppercase.Length)],
                lowercase[random.Next(lowercase.Length)],
                lowercase[random.Next(lowercase.Length)],
                digits[random.Next(digits.Length)],
                digits[random.Next(digits.Length)],
                special[random.Next(special.Length)],
                special[random.Next(special.Length)],
            ];
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
    }
}
