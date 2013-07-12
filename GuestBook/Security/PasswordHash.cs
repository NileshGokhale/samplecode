using System;
using System.Security.Cryptography;

namespace GuestBook.Security
{
    /// <summary>
    /// Class creates password hash.
    /// </summary>
    public class PasswordHash
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        public const int Pbkdf2Iterations = 1000;

        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        /// <summary>
        /// Creates the hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string CreateHash(string password)
        {
            var csprng = RandomNumberGenerator.Create();
            var salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            byte[] hash = Pbkfd2(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);

        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="correctHash">The correct hash.</param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[Pbkdf2Index]);
            byte[] testHash = Pbkfd2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Slows the equals.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length - (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        /// <summary>
        /// PBKFD2s the specified password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The iterations.</param>
        /// <param name="outputBytes">The output bytes.</param>
        /// <returns></returns>
        private static byte[] Pbkfd2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) {IterationCount = iterations};
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}