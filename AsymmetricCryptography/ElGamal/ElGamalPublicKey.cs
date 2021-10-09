using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.ElGamal
{
    class ElGamalPublicKey : AsymmetricKey
    {
        public override string KeyType => "El Gamal public key";

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
    }
}
