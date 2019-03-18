using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEncryption
{
    public partial class FrmFileEncrypt : Form
    {
        public FrmFileEncrypt()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Saves the file after encrypt or decrypting 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string data = TxtBoxFileEncrypt.Text;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text File (*.txt)|*.txt";
            dialog.FileName = "example";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                File.WriteAllText(fileName, data);
                MessageBox.Show("Data saved");
            } 
        }

        /// <summary>
        /// This button will import a file the user chooses to the textbox on the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImport_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                var fileStream = dialog.OpenFile();
                
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                    TxtBoxFileEncrypt.Text = fileContent;
                }
            }
        }
        /// <summary>
        /// Encrypts charater data and creates a cesaer cipher
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - offset) % 26) + offset);
        }

        /// <summary>
        /// Encrypts the data
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += Cipher(ch, key);

            return output;
        }

        /// <summary>
        /// Decrypts the data
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }

        /// <summary>
        /// Button click to encrypt the text in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            string text = TxtBoxFileEncrypt.Text;
            string cipherText = Encipher(text, 3);
            TxtBoxFileEncrypt.Text = cipherText;
        }

        /// <summary>
        /// Button click to decrypt the text in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            string text = TxtBoxFileEncrypt.Text;
            string decipherText = Decipher(text, 3);
            TxtBoxFileEncrypt.Text = decipherText;
        }
    }
}
