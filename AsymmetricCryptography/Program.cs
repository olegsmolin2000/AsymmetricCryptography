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
                
                RsaAlgorithm rsa1 = new RsaAlgorithm(q11,q22);

                DSA dsa1 = new DSA(q1, q2);

                byte[] data = Encoding.UTF8.GetBytes(message.ToString());

                ElGamalDigitalSignature sign = dsa1.CreateSignature(data, new SHA_256());
                BigInteger rsaSign = rsa1.CreateSignature(data, new SHA_256());

                bool dsa = dsa1.VerifySignature(data, sign, new SHA_256());
                bool rsa = rsa1.VerifySignature(rsaSign, data, new SHA_256());

                

                var crypt = rsa1.Encrypt(data);
                var decryp = rsa1.Decrypt(crypt);

                

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
