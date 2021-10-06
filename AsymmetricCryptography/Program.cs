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
            DsaDomainParameters para;

            para = KeysGenerator.DsaDomainParametersGeneration(256, 64);

            //para.PrintConsole();

            DsaPublicKey q = new DsaPublicKey(para, 2512512);
            //q.PrintConsole();
            DsaPrivateKey w = new DsaPrivateKey(para, 125125, 1251215);
            w.PrintConsole();
        }
    }
}
