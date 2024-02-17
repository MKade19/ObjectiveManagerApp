using ObjectiveManagerApp.UI.Services.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace ObjectiveManagerApp.UI.Services
{
    public class HashService : IHashService
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        /// <summary>
        /// Generates hash of the given string and salt for the hash.
        /// </summary>
        /// <param name="stringToHash">String that should be converted to hash.</param>
        /// <param name="salt">Output parameter of generated salt.</param>
        /// <returns>Hash of the given string.</returns>
        public string GenerateHash(string stringToHash, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2
            (
                Encoding.UTF8.GetBytes(stringToHash),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );

            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Verifies whether string matches with given hash or not.
        /// </summary>
        /// <param name="stringToCompare">Normal string to compare with the hashed one.</param>
        /// <param name="hashedString">Hashed string.</param>
        /// <param name="salt">Salt of password.</param>
        /// <returns>True if string matches with hash, false if it doesn't.</returns>
        public bool VerifyHash(string hashedString, string hash, byte[]? salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(hashedString, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
