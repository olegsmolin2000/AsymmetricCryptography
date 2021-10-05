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
            RsaPrivateKey q1;
            RsaPublicKey q2;
            KeysGenerator.RsaKeysGeneration(512, out q1, out q2);
            q1.PrintConsole();
            q2.PrintConsole();
        }
    }
}
