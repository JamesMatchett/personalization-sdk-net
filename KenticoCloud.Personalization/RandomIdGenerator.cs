using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Class for generating IDs suitable for example as an visitor's uid in Kentico Cloud tracking API.
    /// </summary>
    public class RandomIdGenerator
    {
        private readonly SHA1 _sha1Manager = SHA1.Create();

        /// <summary>
        /// Generates an alphanumeric 16 characters long random ID.
        /// </summary>
        /// <returns>Generated ID</returns>
        public string Generate()
        {
            var sha1 = _sha1Manager.ComputeHash(Guid.NewGuid().ToByteArray());

            return GetStringFromByteArray(sha1).Substring(0, 16);
        }

        private string GetStringFromByteArray(IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();

            bytes.ToList().ForEach(b => sb.Append(b.ToString("x2")));

            return sb.ToString();
        }
    }
}
