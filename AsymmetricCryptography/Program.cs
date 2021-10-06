using System;
using System.Numerics;
using System.Text;
using System.Threading;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int s = 0;
            int count = 100000;

            for (int i = 0; i < count; i++)
            {
                if (i % 100 == 0)
                    Console.WriteLine(i);
                RsaPrivateKey q1;
                RsaPublicKey q2;

                KeysGenerator.RsaKeysGeneration(32, out q1, out q2);

                StringBuilder message = new StringBuilder();

                int mesLength = rand.Next() % 500;

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 256)));
                }
                //Console.WriteLine(message.ToString());

                byte[] mes = Encoding.UTF8.GetBytes(message.ToString());

                BigInteger sign = RsaAlgorithm.SignatureCreating(mes, q1, new SHA_256());
                //Console.WriteLine(RsaAlgorithm.SignatureVerification(sign, mes, q2, new SHA_256()));
                if (RsaAlgorithm.SignatureVerification(sign, mes, q2, new SHA_256()))
                    s++;

            }
            Console.WriteLine("succesfull:" + s);
            Console.WriteLine("count:" + count);
            Console.WriteLine("error:{0}%", (1 - ((float)s / (float)count)));
        }
    }
}
