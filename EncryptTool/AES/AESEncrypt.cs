using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptTool.AES
{
    class AESEncrypt
    {
        private AesCryptoServiceProvider Aes;

        public AesCryptoServiceProvider Aes1 { get => Aes; set => Aes = value; }

        public AESEncrypt()
        {
            Aes1 = new AesCryptoServiceProvider();
        }
        public AESEncrypt(AesCryptoServiceProvider Aes) {
            this.Aes1 = Aes;
        }
        public string Encrypt(string plainText, out string key, out string iv)
        {
            Aes1.BlockSize = 128;
            Aes1.KeySize = 256;
            Aes1.Padding = PaddingMode.PKCS7;
            Aes1.Mode = CipherMode.CBC;
            Aes1.GenerateKey();
            Aes1.GenerateIV();
            key = ASCIIEncoding.ASCII.GetString(Aes1.Key);
            iv = ASCIIEncoding.ASCII.GetString(Aes1.IV);
            byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
            ICryptoTransform transform = Aes1.CreateEncryptor(Aes1.Key, Aes1.IV);
            byte[] encryptBytes = transform.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            string outPut = Convert.ToBase64String(encryptBytes);
            return outPut;
        }
        public string Decrypt(string encryptedText, string key, string iv)
        {
            Aes1.BlockSize = 128;
            Aes1.KeySize = 256;
            Aes1.Padding = PaddingMode.PKCS7;
            Aes1.Mode = CipherMode.CBC;
            Aes1.Key = ASCIIEncoding.ASCII.GetBytes(key);
            Aes1.IV = ASCIIEncoding.ASCII.GetBytes(iv);
            ICryptoTransform transform = Aes1.CreateDecryptor(Aes1.Key, Aes1.IV);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            string outPut = ASCIIEncoding.ASCII.GetString(decryptBytes);
            return outPut;
        }
    }
}
