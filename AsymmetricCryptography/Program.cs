using System;
using System.Numerics;
using System.Text;
using System.Threading;
using AsymmetricCryptography.RSA;

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
                //Console.WriteLine(new string('-', 25));
                if (i % 100 == 0)
                    Console.WriteLine(i);
                int size = rand.Next() % 50;

                StringBuilder message = new StringBuilder();

                for (int j = 0; j < size; j++)
                {
                    message.Append(Convert.ToChar(rand.Next() % 255+ 1));
                }

                string mes = message.ToString();
                //mes = "zhopa o4ko mocha huy ebalo";
                RsaPrivateKey q1;
                RsaPublicKey q2;
                KeysGenerator.RsaKeysGeneration(32, out q1, out q2);
                byte[] crypt = RsaAlgorithm.Encrypt(Encoding.UTF8.GetBytes(mes), q2);
                string decrypt = Encoding.Default.GetString(RsaAlgorithm.Decryption(crypt, q1));
                
                if (mes == decrypt)
                    s++;
                else
                {
                    Console.WriteLine("mes:" + mes);
                    //Console.WriteLine(mes.Length);
                    Console.WriteLine("decrypt:" + decrypt);
                    //Console.WriteLine(decrypt.Length);
                    //q1.PrintConsole();
                    //q2.PrintConsole();
                }
                //Console.WriteLine(new string('-', 25));
            }
            Console.WriteLine("s:" + s);
            Console.WriteLine("count:" + count);
            Console.WriteLine("error:" + (1-(float)s/(float)count)*100+"%");
        }
    }
}
