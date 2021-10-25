using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    public class DsaPrivateKey :AsymmetricKey
    {
        public DsaDomainParameters Parameters { get; }

        public BigInteger X { get; }
        public BigInteger Y { get; }

        public override string AlgorithmName => "DSA";
        public override string Permittion => "Private";

        public DsaPrivateKey(DsaDomainParameters parameters,BigInteger x ,BigInteger y)
        {
            this.Parameters = parameters;
            this.X = x;
            this.Y = y;
        }

        public override void PrintConsole()
        {
            Console.WriteLine(new string('-', 75));

            Console.WriteLine("Type: DSA private key");

            Parameters.PrintConsole();

            Console.WriteLine("Private key(X):{0}({1} bits)\n", X, BinaryConverter.GetBinaryLength(X));
            Console.WriteLine("Public key(Y):{0}({1} bits)", Y, BinaryConverter.GetBinaryLength(Y));

            Console.WriteLine(new string('-', 75));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Modulus(n):" + X + " (" + BinaryConverter.GetBinaryLength(X) + " bits)\n");
            //result.Append("Exponent(e):" + Y + " (" + BinaryConverter.GetBinaryLength(Y) + " bits)\n");

            return result.ToString();
        }
    }
}
