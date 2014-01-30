using System;
using System.Text;
using System.Security.Cryptography;

namespace msgen.security
{
    public class Hasher
    {
        static public byte[] sha1(string source)
        {
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(source));
            return hash;
        }

        public static string sha1(string source, bool separated)
        {
            string hash = BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(source)));
            if(!separated)
                hash = hash.Replace("-", string.Empty);
            return hash;
        }

        public static byte[] md5(string source)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(source));
            return hash;
        }

        public static string md5(string source, bool separated)
        {
            string hash = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(source)));
            if(!separated)
                hash = hash.Replace("-", string.Empty);
            return hash;
        }

    }
}
