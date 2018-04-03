using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    class SteganographyProjectClass
    {
       //private string IV = "qgp065mlsy3ep064"; // 16 chars = 128 bytes

        public string OpenPicture()
        {
            string url = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg|PNG|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                url = ofd.FileName;
                return url;
            }
            return url;
        }
        public void SavePicture(Bitmap img)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPG|*.jpg|PNG|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string url = sfd.FileName;
                img.Save(url);
            }

        }

        public string Encrypt_TriplDES(string key, string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf8.GetBytes(key));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateEncryptor();
            return Convert.ToBase64String(trans.TransformFinalBlock(utf8.GetBytes(text), 0, utf8.GetBytes(text).Length));
        }

        public string Decrypt_TriplDES(string key, string text)
        {
            byte[] encrypted = Convert.FromBase64String(text);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf8.GetBytes(key));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateDecryptor();
            try
            {
                return utf8.GetString(trans.TransformFinalBlock(encrypted, 0, encrypted.Length));
            }
            catch
            {
                return "";
            }
        }

        //public string Encrypt_AES(string key, string text) // key -> 32 chars = 256 bytes
        //{
        //    byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(text);
        //    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        //    aes.BlockSize = 128;
        //    aes.KeySize = 256;
        //    aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
        //    aes.IV = ASCIIEncoding.ASCII.GetBytes(IV);
        //    aes.Padding = PaddingMode.PKCS7;
        //    aes.Mode = CipherMode.CBC;
        //    ICryptoTransform trans = aes.CreateEncryptor(aes.Key, aes.IV);

        //    byte[] encrypt = trans.TransformFinalBlock(textbytes, 0, textbytes.Length);
        //    trans.Dispose();

        //    return Convert.ToBase64String(encrypt);

        //}

        //public string Decrypt_AES(string key, string text) // key -> 32 chars = 256 bytes
        //{
        //    byte[] textbytes = Convert.FromBase64String(text);
        //    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        //    aes.BlockSize = 128;
        //    aes.KeySize = 256;
        //    try
        //    {
        //        aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
        //        aes.IV = ASCIIEncoding.ASCII.GetBytes(IV);
        //        aes.Padding = PaddingMode.PKCS7;
        //        aes.Mode = CipherMode.CBC;
        //        ICryptoTransform trans = aes.CreateDecryptor(aes.Key, aes.IV);

        //        byte[] decrypt = trans.TransformFinalBlock(textbytes, 0, textbytes.Length);
        //        trans.Dispose();

        //        return ASCIIEncoding.ASCII.GetString(decrypt);
        //    }
        //    catch
        //    {
        //        return "";
        //    }


        //}
        //public string Key(string text)
        //{
        //    string k = text;
        //    string key = "";
        //    if (k.Length == 0)
        //    {
        //        for (int i = 0; i < 32; i++)
        //        {
        //            key += " ";
        //        }
        //    }
        //   else if (k.Length < 32)
        //    {
        //        for (int i = 0; i < 32 - k.Length - 1; i++)
        //        {
        //            key += " ";
        //        }
        //    }
        //    else key = k;
        //    return key;
        //}

    }
}
