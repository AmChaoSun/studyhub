using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace StudyHub.Utils
{
    public class HashHelper
    {
        private readonly IConfiguration configuration;

        public HashHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetHashedData(string text)
        {
            string key = configuration["PwdHash:Key"];
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
            {
                hashBytes = hash.ComputeHash(textBytes);
                return BitConverter.ToString(hashBytes);
            }
        }

        public bool ValidateHash(string inputData, string storedHash)
        {
            string getHashInputData = GetHashedData(inputData);
            if (string.Compare(getHashInputData, storedHash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
