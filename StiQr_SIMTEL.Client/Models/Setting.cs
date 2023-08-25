using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace StiQr_SIMTEL.Client.Models
{
    internal class Setting
    {
        public static UserBasicDetail UserBasicDetail { get; set; }

    }


    public class UrlEncryptor
    {
        private readonly string key;

        public UrlEncryptor(string key)
        {
            this.key = key;
        }

        public string EncryptUrl(string url)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16];
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] urlBytes = Encoding.UTF8.GetBytes(url);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(urlBytes, 0, urlBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public string DecryptUrl(string encryptedUrl)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16];
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedUrl);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
