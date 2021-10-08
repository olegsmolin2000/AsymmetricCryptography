using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    class DsaPublicKey : AsymmetricKey
    {
        public DsaDomainParameters Parameters { get; }

        public BigInteger Y { get; }

        public override string KeyType => "DSA public key";

        public DsaPublicKey(DsaDomainParameters parameters,BigInteger y)
        {
            this.Parameters = parameters;
            this.Y = y;
        }

        public override void PrintConsole()
        {
            Console.WriteLine(new string('-', 75));

            Console.WriteLine("Type: DSA public key");

            Parameters.PrintConsole();

            Console.WriteLine("Public key(Y):{0}({1} bits)", Y, BinaryConverter.GetBinaryLength(Y));

            Console.WriteLine(new string('-', 75));
        }
    }
}
