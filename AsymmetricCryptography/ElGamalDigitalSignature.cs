using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography
{
    //цифровая подпись по схеме Эль Гамаля
    class ElGamalDigitalSignature
    {
        public BigInteger R { get; }
        public BigInteger S { get; }

        public ElGamalDigitalSignature(BigInteger r, BigInteger s)
        {
            this.R = r;
            this.S = s;
        }

        public void PrintConsole()
        {
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Type: ElGamal scheme digital signature");

            Console.WriteLine("R:{0}({1} bits)\n", R, BinaryConverter.GetBinaryLength(R));
            Console.WriteLine("S:{0}({1} bits)", S, BinaryConverter.GetBinaryLength(S));

            Console.WriteLine(new string('-', 50));
        }
    }
}
