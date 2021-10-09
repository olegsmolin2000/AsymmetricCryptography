using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.ElGamal
{
    class ElGamalKeyParameters
    {
        public BigInteger P { get; }
        public BigInteger G { get; }

        public ElGamalKeyParameters(BigInteger p,BigInteger g)
        {
            this.P = p;
            this.G = g;
        }

        public void PrintConsole()
        {
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("El Gamal key parameters:");

            Console.WriteLine("P:{0}({1} bits)\n", P, BinaryConverter.GetBinaryLength(P));
            Console.WriteLine("G:{0}({1} bits)", G, BinaryConverter.GetBinaryLength(G));

            Console.WriteLine(new string('-', 50));
        }
    }
}
