using System;
using System.Security.Cryptography;
using System.Text;

namespace CarRent.Common.Authentication.Helpers
{
    public class PasswordHasher
    {
        private const string SECRET = "123456789";
        public static string Hash(string value)
        {
            Encoding encoding = Encoding.UTF8;
            using (HMACMD5 hmac = new HMACMD5(encoding.GetBytes(SECRET)))
            {
                var msg = encoding.GetBytes(value);
                var hash = hmac.ComputeHash(msg);
                return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }
    }
}
