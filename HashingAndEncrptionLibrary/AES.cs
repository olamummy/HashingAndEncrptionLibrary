
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashingAndEncrptionLibrary
{
    public class AES
    {
        //Method for Encryption
        public  string Encrypt(string plaintext, string secretkey, string iv)
        {
            try
            {
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key = Convert.FromBase64String(secretkey);
                    myAes.IV = Convert.FromBase64String(iv);
                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes_Aes(plaintext, myAes.Key, myAes.IV);
                    string ciphertext = ByteArrayToString(encrypted);
                    return ciphertext;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Method for Decryption
        public string Decrypt(string ciphertext, string secretKey, string iv)
        {
            try
            {
                // Create a new instance of the Aes class. This generates a new key and initialization vector (IV).
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key = Convert.FromBase64String(secretKey);
                    myAes.IV = Convert.FromBase64String(iv);
                    // Decrypt the bytes to a string.
                    string roundtrip = DecryptStringFromBytes_Aes(ciphertext, myAes.Key, myAes.IV);
                    return roundtrip;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Method for Generating Random IV
        public string GenerateIV()
        {
            byte[] IVbytes;
            string IV;
            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateIV();
                IVbytes = aes.IV;
            }
            IV = Convert.ToBase64String(IVbytes);
            return IV;
        }
        //Method for Generating Random Key
        public  string GenerateKey()
        {
            byte[] KeyBytes;
            string Key;
            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateKey();
                KeyBytes = aes.Key;
            }
            Key = Convert.ToBase64String(KeyBytes);
            return Key;
        }

        private  byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Key;
                aes.IV = IV;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
        private  string DecryptStringFromBytes_Aes(string cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Key;
                aes.IV = IV;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] cipherbytes = HexadecimalStringToByteArray(cipherText);
                using (MemoryStream ms = new MemoryStream(cipherbytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        private  byte[] HexadecimalStringToByteArray(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(),
(char)sr.Read() }), 16);
            }
            return output;
        }
        private  string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b); return hex.ToString();
        }
    }
}