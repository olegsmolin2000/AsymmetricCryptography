using System;
using System.Numerics;
using System.Text;
using System.Threading;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;

namespace AsymmetricCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            int s = 0;
            int count = 1000;

            for (int i = 0; i < count; i++)
            {
                //if (i % 10 == 0)
                    Console.WriteLine(i);
                DsaPrivateKey q1;
                DsaPublicKey q2;
                RsaPrivateKey q11;
                RsaPublicKey q22;
                KeysGenerator.RsaKeysGeneration(24, out q11, out q22);
                KeysGenerator.DsaKeysGeneration(48, 32, out q1, out q2);
                //System.Security.Cryptography.RSACryptoServiceProvider
                StringBuilder message = new StringBuilder();
                AsymmetricKey qq = q11;
                int mesLength = rand.Next() % 50;

                ElGamalPrivateKey qq1;
                ElGamalPublicKey qq2;

                KeysGenerator.ElGamalKeysGeneration(32, out qq1, out qq2);

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 255+1)));
                }
                
                RsaAlgorithm rsa1 = new RsaAlgorithm(q11,q22);
                
                DSA dsa1 = new DSA(q1, q2);

                ElGamalAlgorithm elg = new ElGamalAlgorithm(qq1,qq2);

                byte[] data = Encoding.UTF8.GetBytes(message.ToString());

                DigitalSignature sign = dsa1.CreateSignature(data, new SHA_256());
                DigitalSignature rsaSign = rsa1.CreateSignature(data, new SHA_256());
                DigitalSignature elgamalds = elg.CreateSignature(data, new SHA_256());

                bool dsa = dsa1.VerifyDigitalSignature(sign, data, new SHA_256());
                bool rsa = rsa1.VerifyDigitalSignature(rsaSign, data, new SHA_256());
                bool elgamal = elg.VerifyDigitalSignature(elgamalds, data, new SHA_256());
                

                var crypt = rsa1.Encrypt(data);
                var decryp = rsa1.Decrypt(crypt);

                var egcrypt = elg.Encrypt(data);
                var egdecrypt = elg.Decrypt(egcrypt);


                if (!dsa)
                    Console.WriteLine("dsa error");
                if (!rsa)
                    Console.WriteLine("rsa sign error");
                if (Encoding.UTF8.GetString(decryp) != message.ToString())
                    Console.WriteLine("rsa crypt error");
                if (Encoding.UTF8.GetString(egdecrypt) != message.ToString())
                    Console.WriteLine("el gamal crypt error");
                if (!elgamal)
                    Console.WriteLine("el gamal sign error");

            }


            Console.WriteLine("s:" + s);
            Console.WriteLine("count:" + count);
        }
    }
}
