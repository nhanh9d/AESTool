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
        string PrivateKey = "password";
        string PublicKey = "asdqweasdqweasdqweas";

        private AesCryptoServiceProvider Aes;
        /// <summary>
        /// Dùng để tạo đối tượng cho việc mã hóa
        /// </summary>
        public AESEncrypt()
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Encoding.Unicode.GetBytes(PrivateKey.ToCharArray()), Encoding.Unicode.GetBytes(PublicKey.ToCharArray()));
            Aes = new AesCryptoServiceProvider();
            Aes.BlockSize = 128;
            Aes.KeySize = 256;
            Aes.Mode = CipherMode.CBC;
            Aes.Padding = PaddingMode.PKCS7;
            Aes.Key = pdb.GetBytes(32); // Tạo key ngẫu nhiên
            Aes.IV = pdb.GetBytes(16); // Tạo key ngẫu nhiên
            //Aes.GenerateIV(); // Tạo init vector ngẫu nhiên
        }
        /// <summary>
        /// Dùng để tạo đối tượng cho việc giải mã
        /// </summary>
        /// <param name="_Key">Truyền key đã được nhận</param>
        /// <param name="_IV">Truyền vector đã được nhận</param>
        public AESEncrypt(string _Key, string _IV, PaddingMode pm)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Encoding.Unicode.GetBytes(PrivateKey.ToCharArray()), Encoding.Unicode.GetBytes(PublicKey.ToCharArray()));
            Aes = new AesCryptoServiceProvider();
            Aes.BlockSize = 128;
            Aes.KeySize = 256;
            Aes.Mode = CipherMode.CBC;
            Aes.Padding = pm;
            Aes.Key = pdb.GetBytes(32); // Tạo key ngẫu nhiên
            Aes.IV = pdb.GetBytes(16); // Tạo key ngẫu nhiên
            //Aes.Key = ASCIIEncoding.ASCII.GetBytes(_Key); //Gán lại key đã lưu
            //Aes.IV = ASCIIEncoding.ASCII.GetBytes(_IV); //Gán lại iv đã lưu
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, out string key, out string iv, out PaddingMode pm)
        {
            pm = Aes.Padding;
            key = ASCIIEncoding.ASCII.GetString(Aes.Key); //out key
            iv = ASCIIEncoding.ASCII.GetString(Aes.IV); //out init vector
            ICryptoTransform transform = Aes.CreateEncryptor(); //tạo đối tượng mã hóa
            byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText.Trim()); // chuyển chuỗi nhập vào thành mảng nhị phân
            byte[] encryptBytes = transform.TransformFinalBlock(plainTextBytes, 0, plainText.Length); // làm tròn thành mảng mã hóa (là mũ n của 2)
            string outPut = Convert.ToBase64String(encryptBytes); //Chuyển mảng mã hóa thành chuỗi mã hóa
            return outPut; //Trả chuỗi đã được mã hóa
        }
        public string Decrypt(string encryptedText)
        {
            ICryptoTransform transform = Aes.CreateDecryptor(); //Tạo đối tượng giải mã
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText); //Chuyển chuỗi mã hóa thành mảng nhị phân
            byte[] decryptBytes = transform.TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length); // làm tròn thành mảng giải mã
            string outPut = ASCIIEncoding.ASCII.GetString(decryptBytes); //Chuyển mảng giải mã thành chuỗi giải mã
            return outPut; //Trả chuỗi đã được giải mã
        }
    }
}
