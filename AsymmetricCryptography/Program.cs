using System;
using System.Numerics;
using System.Text;
using System.Threading;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.DigitalSignatureAlgorithm;

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
                if (i % 50 == 0)
                    Console.WriteLine(i);
                DsaPrivateKey q1;
                DsaPublicKey q2;

                RsaPrivateKey q11;
                RsaPublicKey q22;

                KeysGenerator.RsaKeysGeneration(32, out q11, out q22);
                KeysGenerator.DsaKeysGeneration(128, 64, out q1, out q2);

                StringBuilder message = new StringBuilder();
                
                int mesLength = rand.Next() % 50;

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 255+1)));
                }

                byte[] data = Encoding.UTF8.GetBytes(message.ToString());

                ElGamalDigitalSignature sign = DSA.CreateSignature(data, q1, new SHA_256());
                BigInteger rsaSign = RSA.RsaAlgorithm.CreateSignature(data, q11, new SHA_256());

                bool dsa = DSA.VerifySignature(data, sign, q2, new SHA_256());
                bool rsa = RsaAlgorithm.VerifySignature(rsaSign, data, q22, new SHA_256());

                RsaAlgorithm rsa1 = new RsaAlgorithm(q11, q22);

                var crypt = rsa1.Encrypt(data);
                var decryp = RsaAlgorithm.Decrypt(crypt, q11);

                

                if (!dsa)
                    Console.WriteLine("dsa error");
                if (!rsa)
                    Console.WriteLine("rsa sign error");
                if (Encoding.UTF8.GetString(decryp) != message.ToString())
                    Console.WriteLine("rsa crypt error");
                //else
                    //Console.WriteLine("rsa crypt success");
            }


            Console.WriteLine("s:" + s);
            Console.WriteLine("count:" + count);
        }
    }
}
