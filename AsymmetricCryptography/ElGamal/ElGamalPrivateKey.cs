using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.ElGamal
{
    class ElGamalPrivateKey : AsymmetricKey
    {
        public override string KeyType => "El Gamal private Key";

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
    }
}
