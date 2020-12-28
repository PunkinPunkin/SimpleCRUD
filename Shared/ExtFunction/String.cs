using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Shared
{
    /// <summary>
    /// String 的擴充方法
    /// </summary>
    public static partial class ExtFunction
    {
        static char[] PassPhrase { get { return new char[] { 'P', 'a', 's', '5', 'p', 'h', 'r', '@', 's', 'e' }; } }

        const string saltValue = "s@1tValue";//can be any string
        const string hashAlgorithm = "SHA1";//can be "MD5"
        const int passwordIterations = 2;//can be any number
        const string initVector = "@1B2c3D4e5F6g7H8";//must be 16 bytes
        const int keySize = 256;//can be 192 or 128

        /// <summary>
        /// 字串加密
        /// </summary>
        /// <param name="plainText">被加密字串</param>
        /// <returns>加密字串</returns>
        public static string Encrypt(this string plainText)
        {
            string cipherText = "";
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes pdb = new PasswordDeriveBytes(new string(PassPhrase), saltValueBytes, hashAlgorithm, passwordIterations))
            {
                byte[] keyBytes = pdb.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherTextBytes = memoryStream.ToArray();
                    memoryStream.Close();
                    cryptoStream.Close();
                    cipherText = Convert.ToBase64String(cipherTextBytes);
                }
            }
            return cipherText;
        }

        public static string Decrypt(this string cipherText)
        {
            string plainText = "";
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes pdb = new PasswordDeriveBytes(new string(PassPhrase), saltValueBytes, hashAlgorithm, passwordIterations))
            {
                byte[] keyBytes = pdb.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                {
                    MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    memoryStream.Close();
                    cryptoStream.Close();
                    plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                }
            }
            return plainText;
        }
    }

    public static partial class ExtFunction
    {
        public static string ToString(this IEnumerable<string> p, StringSplitOptions o = StringSplitOptions.None, char s = '|')
        {
            string[] a = string.Join(s.ToString(), p).Split(new char[] { s }, o);
            return string.Join(string.Empty, a);
        }

        public static string ToRandomString(this string str, int length)
        {
            var next = new Random();
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                builder.Append(str[next.Next(0, str.Length)]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 字串base64編碼
        /// </summary>
        /// <param name="InpuString"></param>
        public static string EncodeFrom64(this string InpuString)
        {
            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(InpuString);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }

}
