using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography.RSA
{
    class RsaKey
    {
        public BigInteger Exponent { get; }
        public BigInteger Modulus { get; }

        public RsaKey(BigInteger exponent,BigInteger modulus)
        {
            this.Exponent = exponent;
            this.Modulus = modulus;
        }

        public void PrintConsole()
        {
            //TODO
            //Console.WriteLine("Key RSA:");
            //Console.WriteLine("Exponent:{0} (size: {1} bits)", Exponent, BinaryConverter.GetBinaryLength(Exponent));
            //Console.WriteLine("Modulus:{0} (size: {1} bits)", Modulus, BinaryConverter.GetBinaryLength(Modulus));
        }
    }
}
