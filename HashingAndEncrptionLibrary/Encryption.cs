using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashingAndEncrptionLibrary
{
   public class Encryption
    {
        private static string KEY = "xlprghyowrpalvnxeuglhjsaqruiolms";
        private static string IV = "xplpaymentfjtrdk";

        //string IV = "B464E517-EB3C-43E8-BCF6-845E6C5A7132";
        //string KEY = "CLIENT-521C68C0-8EC0-11EA-9E71-3FC4F8C2C1E8";


        #region Enryption Pattern for encrypting objects
        public string Encrypt(string value)
        {
            byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY);
            aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            //crypto.Dispose();
            byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            return Convert.ToBase64String(encrypted);

        }

        #endregion

        #region Decrytion Pattern for decripting objects
        public string Decrypt(string encrypt)
        {
            byte[] encrytedbytes = Convert.FromBase64String(encrypt);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY);
            aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            // crypto.Dispose();
            byte[] rawData = new byte[encrytedbytes.Length];
            //byte[] decrypt = crypto.TransformFinalBlock(encrytedbytes, 0, encrytedbytes.Length);
            return System.Text.ASCIIEncoding.ASCII.GetString(rawData);
        }
        #endregion


        public string DecryptAES(string Text)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Key = Encoding.UTF8.GetBytes(KEY);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] src = System.Convert.FromBase64String(Text);
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.UTF8.GetString(dest);
            }
        }

        public string uuu(string text)
        {
            byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(text);

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;          // in bits
                aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                                              // Should set Key and IV here.  Good approach: derive them from 
                                              // a password via Cryptography.Rfc2898DeriveBytes 
                byte[] cipherText = null;
                byte[] plainText = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                    }

                    cipherText = ms.ToArray();
                }


                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                    }

                    plainText = ms.ToArray();
                }
                string s = System.Text.Encoding.Unicode.GetString(plainText);
                //Console.WriteLine(s);
                return s;
            }
        }

    }
}
