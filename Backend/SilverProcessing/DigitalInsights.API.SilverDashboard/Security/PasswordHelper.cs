using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Security
{
    public static partial class PasswordHelper
    {
        public static byte Version => 1;
        public static int Pbkdf2IterationCount { get; } = 50000;
        public static int Pbkdf2SubkeyLength { get; } = 256 / 8; // 256 bits
        public static int SaltSize { get; } = 128 / 8; // 128 bits
        public static HashAlgorithmName HashAlgorithmName { get; } = HashAlgorithmName.SHA256;

        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] salt;
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterationCount, HashAlgorithmName))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(Pbkdf2SubkeyLength);
            }

            var inArray = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            inArray[0] = Version;
            Buffer.BlockCopy(salt, 0, inArray, 1, SaltSize);
            Buffer.BlockCopy(bytes, 0, inArray, 1 + SaltSize, Pbkdf2SubkeyLength);

            return Convert.ToBase64String(inArray);
        }

        public static PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            if (hashedPassword == null)
                return PasswordVerificationResult.Failed;

            byte[] numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length < 1)
                return PasswordVerificationResult.Failed;

            byte version = numArray[0];
            if (version > Version)
                return PasswordVerificationResult.Failed;

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(numArray, 1, salt, 0, SaltSize);
            byte[] a = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(numArray, 1 + SaltSize, a, 0, Pbkdf2SubkeyLength);
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, Pbkdf2IterationCount, HashAlgorithmName))
            {
                bytes = rfc2898DeriveBytes.GetBytes(Pbkdf2SubkeyLength);
            }

            return CryptographicOperations.FixedTimeEquals(a, bytes)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
