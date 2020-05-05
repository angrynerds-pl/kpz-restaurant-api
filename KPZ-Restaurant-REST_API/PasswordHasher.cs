using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KPZ_Restaurant_REST_API
{
    public sealed class PasswordHasher
        {
        private const int SaltSize = 16;
        private const int HashSize = 20;

        public static string HashPassword(string password, int iterations)
        {
            byte[] salt = new byte[SaltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return string.Format("$RESTAURANT$HASH${0}${1}", iterations, base64Hash);
        }

        public static string HashPassword(string password)
        {
            return HashPassword(password, 1000);
        }

        public static bool IsHashSupported(string hash)
        {
            return hash.Contains("$RESTAURANT$HASH$");
        }

        public static bool ComparePassword(string password, string hashedPassword)
        {
            if (!IsHashSupported(hashedPassword))
            {
                return false;
            }

            var splittedHashString = hashedPassword.Replace("$RESTAURANT$HASH$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            var hashBytes = Convert.FromBase64String(base64Hash);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }


    }
}
