using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Text;

namespace HackTonTemplate.Extensions
{
    public static class StringExtensions
    {
        public static string ToSha256(this string value)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(value)));
        }
    }
}
