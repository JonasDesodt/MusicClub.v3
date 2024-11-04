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
            var apiKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); // Random API Key
            var salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);

            var hashedApiKey = HashApiKey(apiKey, salt);

            return (apiKey, hashedApiKey, salt);
        }

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
