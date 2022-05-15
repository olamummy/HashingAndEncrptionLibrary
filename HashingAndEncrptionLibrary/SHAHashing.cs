using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashingAndEncrptionLibrary
{
    public enum Supported_Hash
    {
        SHA256, SHA384, SHA512, MD5
    }
    public class SHAHashing
    {
        public string ComputeHash(string plaintext, Supported_Hash hash, byte[] salt)
        {
            int minSaltLength = 4;
            int maxSaltLength = 16;

            byte[] SaltBytes = null;
            if (salt != null)
            {
                SaltBytes = salt;
            }
            else
            {
                Random r = new Random();
                int SaltLengh = r.Next(minSaltLength, maxSaltLength);
                SaltBytes = new byte[SaltLengh];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(SaltBytes);
                rng.Dispose();
            }

            byte[] plainData = ASCIIEncoding.UTF8.GetBytes(plaintext);
            byte[] plainDataAndSalt = new byte[plainData.Length + SaltBytes.Length];

            for (int x = 0; x < plainData.Length; x++)
                plainDataAndSalt[x] = plainData[x];
            for (int n = 0; n < SaltBytes.Length; n++)
                plainDataAndSalt[plainData.Length + n] = SaltBytes[n];

            byte[] hashValue = null;

            switch (hash)
            {
                case Supported_Hash.SHA256:
                    SHA256Managed sha = new SHA256Managed();
                    hashValue = sha.ComputeHash(plainDataAndSalt);
                    sha.Dispose();
                    break;

                case Supported_Hash.SHA384:
                    SHA384Managed sha1 = new SHA384Managed();
                    hashValue = sha1.ComputeHash(plainDataAndSalt);
                    sha1.Dispose();
                    break;
                case Supported_Hash.SHA512:
                    SHA512Managed sha2 = new SHA512Managed();
                    hashValue = sha2.ComputeHash(plainDataAndSalt);
                    sha2.Dispose();
                    break;
            }

            byte[] result = new byte[hashValue.Length + SaltBytes.Length];

            for (int x = 0; x < hashValue.Length; x++)
                result[x] = hashValue[x];
            for (int n = 0; n < SaltBytes.Length; n++)
                result[hashValue.Length + n] = SaltBytes[n];
            return Convert.ToBase64String(result);

        }

        public bool Confirm(string plainText, string hashValue, Supported_Hash hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hashValue);
            int hashSize = 0;
            switch (hash)
            {
                case Supported_Hash.SHA256:
                    hashSize = 32;
                    break;
                case Supported_Hash.SHA384:
                    hashSize = 48;
                    break;
                case Supported_Hash.SHA512:
                    hashSize = 64;
                    break;
            }

            byte[] saltBytes = new byte[hashBytes.Length - hashSize];

            for (int x = 0; x < saltBytes.Length; x++)
                saltBytes[x] = hashBytes[hashSize + x];
            string NewHash = ComputeHash(plainText, hash, saltBytes);
            return (hashValue == NewHash);

        }

        public string HashValue(string plainValue, Supported_Hash hashPattern)
        {
            string hashstring = string.Empty;
            switch (hashPattern)
            {
                case Supported_Hash.SHA256:
                    byte[] bytes0 = Encoding.UTF8.GetBytes(plainValue);
                    System.Security.Cryptography.SHA256Managed sha256hashstring1 = new System.Security.Cryptography.SHA256Managed();
                    byte[] hash0 = sha256hashstring1.ComputeHash(bytes0);
                    foreach (byte x in hash0)
                    {
                        hashstring += string.Format("{0:x2}", x);
                    }
                    break;

                case Supported_Hash.SHA384:
                    byte[] bytes1 = Encoding.UTF8.GetBytes(plainValue);
                    System.Security.Cryptography.SHA384Managed sha384hashstring1 = new System.Security.Cryptography.SHA384Managed();
                    byte[] hash1 = sha384hashstring1.ComputeHash(bytes1);
                    foreach (byte x in hash1)
                    {
                        hashstring += string.Format("{0:x2}", x);
                    }
                    break;
                case Supported_Hash.SHA512:
                    byte[] bytes = Encoding.UTF8.GetBytes(plainValue);
                    System.Security.Cryptography.SHA512Managed sha512hashstring = new System.Security.Cryptography.SHA512Managed();
                    byte[] hash = sha512hashstring.ComputeHash(bytes);
                    foreach (byte x in hash)
                    {
                        hashstring += string.Format("{0:x2}", x);
                    }
                    break;
                case Supported_Hash.MD5:
                    using (System.Security.Cryptography.MD5 mdhash = System.Security.Cryptography.MD5.Create())
                    {
                        string hashmd5 = GetMD5Hash(mdhash, plainValue);
                        hashstring = hashmd5;
                    }
                    break;
                default:
                    break;
            }


            return hashstring;
        }

        static string GetMD5Hash(System.Security.Cryptography.MD5 md5hash, string input)
        {
            byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sbuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sbuilder.Append(data[i].ToString("x2"));
            }
            return sbuilder.ToString();

        }
    }
}
