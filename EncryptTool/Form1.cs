using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptTool.AES;
using System.Security.Cryptography;

namespace EncryptTool
{
    public partial class Form1 : Form
    {
        private string key = "";
        private string InitVector = "";
        private string encryptText = "";
        private PaddingMode pm;

        AesCryptoServiceProvider provider = new AesCryptoServiceProvider();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            provider.BlockSize = 128;
            provider.KeySize = 256;
            provider.GenerateIV();
            provider.GenerateKey();
            provider.Mode = CipherMode.CBC;
            provider.Padding = PaddingMode.PKCS7;
        }
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            //ICryptoTransform transform = provider.CreateEncryptor();
            //byte[] encryptTextBytes = ASCIIEncoding.ASCII.GetBytes(rtbInput.Text.Trim());
            //byte[] encryptBytes = transform.TransformFinalBlock(encryptTextBytes, 0, rtbInput.Text.Length);
            //rtbOutput.Text = encryptText = Convert.ToBase64String(encryptBytes);

            AESEncrypt aes = new AESEncrypt();
            rtbOutput.Text = encryptText = aes.Encrypt(rtbInput.Text, out key, out InitVector, out pm);
            lbPublicKey.Text = key;
            lbPrivateKey.Text = InitVector;
        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            //ICryptoTransform transform = provider.CreateDecryptor();
            //byte[] decryptTextBytes = Convert.FromBase64String(encryptText);
            //byte[] decryptBytes = transform.TransformFinalBlock(decryptTextBytes, 0, decryptTextBytes.Length);
            //rtbOutput.Text = ASCIIEncoding.ASCII.GetString(decryptBytes);

            AESEncrypt aes = new AESEncrypt(key, InitVector, pm);
            rtbOutput.Text = aes.Decrypt(encryptText);
        }

    }
}
