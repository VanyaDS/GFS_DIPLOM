using System.Security.Cryptography;
using System.Text;

namespace GeoFlat.Server.Helpers
{
    internal static class HashingMD5
    {
        public static string GetHashStringMD5(string stringToEncrypt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(stringToEncrypt);

            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
