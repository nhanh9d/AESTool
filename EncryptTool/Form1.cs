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

namespace EncryptTool
{
    public partial class Form1 : Form
    {
        private string key = "";
        private string InitVector = "";
        private string encryptText = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            AESEncrypt aes = new AESEncrypt();
            rtbOutput.Text = aes.Decrypt(encryptText, key, InitVector);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            AESEncrypt aes = new AESEncrypt();
            encryptText = aes.Encrypt(rtbInput.Text, out key, out InitVector);
            lbPublicKey.Text = key;
            lbPrivateKey.Text = InitVector;
            MessageBox.Show(encryptText);
        }
    }
}
