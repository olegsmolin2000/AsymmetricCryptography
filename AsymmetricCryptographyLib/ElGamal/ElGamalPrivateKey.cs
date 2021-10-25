using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.ElGamal
{
    public class ElGamalPrivateKey : AsymmetricKey
    {
        public override string AlgorithmName => "ElGamal";
        public override string Permittion => "Private";

        public ElGamalKeyParameters Parameters { get; }

        public BigInteger X { get; }

        public ElGamalPrivateKey(ElGamalKeyParameters parameters,BigInteger x)
        {
            this.Parameters = parameters;
            this.X = x;
        }

        public override void PrintConsole()
        {
            Console.WriteLine(new string('-', 75));

            Console.WriteLine("Type: El Gamal private key");

            Parameters.PrintConsole();

            Console.WriteLine("Public key(X):{0}({1} bits)", X, BinaryConverter.GetBinaryLength(X));

            Console.WriteLine(new string('-', 75));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("SecretKey(x):" + X + " (" + BinaryConverter.GetBinaryLength(X) + " bits)\n");

            return result.ToString();
        }
    }
}
