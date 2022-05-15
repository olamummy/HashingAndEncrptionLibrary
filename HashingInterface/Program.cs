using HashingAndEncrptionLibrary;
using System;

namespace HashingInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Encryption encryption = new Encryption();
                SHAHashing sHAHashing = new SHAHashing();
                AES eEShhh = new AES();

                string md5_key = "B464E517-EB3C-43E8-BCF6-845E6C5A7132";
                string value =  "CLIENT-521C68C0-8EC0-11EA-9E71-3FC4F8C2C1E8";

                string data = "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";

                // r = eEShhh.Decrypt(data, md5_key, value);
                //var kk = encryption.Decrypt(data);
                var kk = encryption.DecryptAES(data);

                ////////////////////////////Encrypt a password//////////////////////////
                Console.WriteLine("Encrypt a password");

                var encryptResult = encryption.Encrypt(Console.ReadLine());

                Console.WriteLine("Result of Encyption " + encryptResult);

                Console.WriteLine("------------------------------------------------------------------");

                /////////////////////////////Decrypt an encrypted password////////////////////////////////
                Console.WriteLine("Decrypt an encrypted password");

                var decryptresult = encryption.Decrypt(Console.ReadLine());

                Console.WriteLine("Result of Decryption " + decryptresult);

                Console.WriteLine("------------------------------------------------------------------");
                /////////////////////////////SHA512////////////////////////////////
                Console.WriteLine("SHA512");

                var SHA512result = sHAHashing.ComputeHash(Console.ReadLine(), Supported_Hash.SHA512, BitConverter.GetBytes(125));

                Console.WriteLine("Result of SHA512 " + SHA512result);

                Console.WriteLine("------------------------------------------------------------------");
                /////////////////////////////SHA256////////////////////////////////
                Console.WriteLine("SHA256");

                var SHA256result = sHAHashing.ComputeHash(Console.ReadLine(), Supported_Hash.SHA256, BitConverter.GetBytes(125));

                Console.WriteLine("Result of SHA256 " + SHA256result);

                Console.WriteLine("------------------------------------------------------------------");
                /////////////////////////////SHA384////////////////////////////////
                Console.WriteLine("SHA384");

                var SHA384result = sHAHashing.ComputeHash(Console.ReadLine(), Supported_Hash.SHA384, BitConverter.GetBytes(125));

                Console.WriteLine("Result of SHA384 " + SHA384result);

                Console.WriteLine("------------------------------------------------------------------");
                
                System.Threading.Thread.Sleep(2300000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex);
                System.Threading.Thread.Sleep(2300000);
            }

        }       
    }
}
