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

                KeysGenerator.DsaKeysGeneration(128, 64, out q1, out q2);

                StringBuilder message = new StringBuilder();

                int mesLength = rand.Next() % 500;

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 256)));
                }

                ElGamalDigitalSignature sign = DSA.CreateSignature(Encoding.UTF8.GetBytes(message.ToString()), q1, new SHA_256());

                Console.WriteLine(DSA.VerifySignature(Encoding.UTF8.GetBytes(message.ToString()), sign, q2, new SHA_256()));
                    s++;


            }


            Console.WriteLine("s:" + s);
            Console.WriteLine("count:" + count);
        }
    }
}
