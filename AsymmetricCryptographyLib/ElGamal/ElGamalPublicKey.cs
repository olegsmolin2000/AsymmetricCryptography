using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.ElGamal
{
    public class ElGamalPublicKey : AsymmetricKey
    {
        public override string AlgorithmName => "ElGamal";
        public override string Permittion => "Public";

        public ElGamalKeyParameters Parameters { get; }

        public BigInteger Y { get; }

        public ElGamalPublicKey(ElGamalKeyParameters parameters, BigInteger y)
        {
            this.Parameters = parameters;
            this.Y = y;
        }

        public override void PrintConsole()
        {
            Console.WriteLine(new string('-', 75));

            Console.WriteLine("Type: El Gamal public key");

            Parameters.PrintConsole();

            Console.WriteLine("Public key(Y):{0}({1} bits)", Y, BinaryConverter.GetBinaryLength(Y));

            Console.WriteLine(new string('-', 75));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("PublicKey(y):" + Y + " (" + BinaryConverter.GetBinaryLength(Y) + " bits)\n");

            return result.ToString();
        }
    }
}
