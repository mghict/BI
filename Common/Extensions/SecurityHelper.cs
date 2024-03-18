using System.Security.Cryptography;
using System.Text;

namespace Moneyon.PowerBi.Common.Extensions
{
    public static class SecurityHelper
    {
        public static byte[] ComputeSha256Hash(string value)
        {
            Encoding enc = Encoding.UTF8;
            byte[] hashBytes;

            using (SHA256 hash = SHA256.Create())
            {
                hashBytes = hash.ComputeHash(enc.GetBytes(value));
            }

            return hashBytes;
        }

        public static string ComputeSha256HashString(string value)
        {
            var sBuilder = new StringBuilder();
            var hash = ComputeSha256Hash(value);
            foreach (var b in hash)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
