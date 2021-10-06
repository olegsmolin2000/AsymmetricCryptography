using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    class DsaPrivateKey
    {
        public DsaDomainParameters Parameters { get; }

        public BigInteger X { get; }
        public BigInteger Y { get; }

        public DsaPrivateKey(DsaDomainParameters parameters,BigInteger x ,BigInteger y)
        {
            this.Parameters = parameters;
            this.X = x;
            this.Y = y;
        }

        public void PrintConsole()
        {
            Console.WriteLine(new string('-', 75));

            Console.WriteLine("Type: DSA private key");

            Parameters.PrintConsole();

            Console.WriteLine("Private key(X):{0}({1} bits)\n", X, BinaryConverter.GetBinaryLength(X));
            Console.WriteLine("Public key(Y):{0}({1} bits)", Y, BinaryConverter.GetBinaryLength(Y));

            Console.WriteLine(new string('-', 75));
        }
    }
}
