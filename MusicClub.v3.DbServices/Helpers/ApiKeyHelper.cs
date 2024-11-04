using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore.Models;
using System.Security.Cryptography;
using System.Text;

namespace MusicClub.v3.DbServices.Helpers
{
    public static class ApiKeyHelper
    {
        public static (string ApiKey, byte[] HashedApiKey, byte[] Salt) GenerateApiKey()
        {
            // Generate a base key with a GUID
            var guidBytes = Guid.NewGuid().ToByteArray();

            // Add extra randomness: 16 additional bytes
            var randomBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Combine the GUID and the additional random bytes
            var apiKeyBytes = new byte[guidBytes.Length + randomBytes.Length];
            Buffer.BlockCopy(guidBytes, 0, apiKeyBytes, 0, guidBytes.Length);
            Buffer.BlockCopy(randomBytes, 0, apiKeyBytes, guidBytes.Length, randomBytes.Length);

            // Encode the combined bytes in Base64 to get the API key string
            var apiKey = Convert.ToBase64String(apiKeyBytes);

            // Generate a random salt for hashing the API key
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the API key with the salt
            var hashedApiKey = HashApiKey(apiKey, salt);

            return (apiKey, hashedApiKey, salt);
        }


        //public static (string ApiKey, byte[] HashedApiKey, byte[] Salt) GenerateApiKey()
        //{
        //    var apiKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); // Random API Key
        //    var salt = new byte[16];
        //    RandomNumberGenerator.Create().GetBytes(salt);

        //    var hashedApiKey = HashApiKey(apiKey, salt);

        //    return (apiKey, hashedApiKey, salt);
        //}

        private static byte[] HashApiKey(string apiKey, byte[] salt)
        {
            using var hmac = new HMACSHA256(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
        }

        public static bool ValidateApiKey(ApiKey apiKeyRecord, string apiKey)
        {
            var incomingHash = HashApiKey(apiKey, apiKeyRecord.Salt);

            return incomingHash.SequenceEqual(apiKeyRecord.HashedApiKey);
        }
    }
}
