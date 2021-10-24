using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    public class DsaDomainParameters
    {
        public BigInteger Q { get; }
        public BigInteger P { get; }
        public BigInteger G { get; }

        public DsaDomainParameters(BigInteger q,BigInteger p,BigInteger g)
        {
            this.Q = q;
            this.P = p;
            this.G = g;
        }

        public void PrintConsole()
        {
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Type: DSA domain parameters\n");

            Console.WriteLine("Q:{0}({1} bits)\n", Q, BinaryConverter.GetBinaryLength(Q));
            Console.WriteLine("P:{0}({1} bits)\n", P, BinaryConverter.GetBinaryLength(P));
            Console.WriteLine("G:{0}({1} bits)", G, BinaryConverter.GetBinaryLength(G));

            Console.WriteLine(new string('-', 50));
        }
    }
}
