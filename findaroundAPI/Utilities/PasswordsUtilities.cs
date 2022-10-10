using System;
using findaroundAPI.Models;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using findaroundAPI.Entities;

namespace findaroundAPI.Utilities
{
	public static class PasswordsUtilities
	{
        public static PasswordHashingModel HashPassword(string password)
        {
            var salt = GenerateSalt();
            var hash = GenerateHash(password, salt);

            return new PasswordHashingModel()
            {
                PasswordHash = hash,
                Salt = salt
            };
        }

        private static string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[16];
            rng.GetBytes(buffer);

            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        private static string GenerateHash(string password, string salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            argon2.Salt = saltBytes;
            argon2.DegreeOfParallelism = 2;
            argon2.Iterations = 4;
            argon2.MemorySize = 512 * 512;

            return Convert.ToBase64String(argon2.GetBytes(16));
        }

        public static bool ArePasswordsEqual(UserEnitity user, string password)
        {
            var secondHash = GenerateHash(password, user.Salt);

            return user.PasswordHash == secondHash;
        }
    }
}

