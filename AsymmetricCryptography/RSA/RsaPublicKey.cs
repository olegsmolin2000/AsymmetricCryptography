using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    class RsaPublicKey
    {
        public BigInteger Exponent { get; }//e
        public BigInteger Modulus { get; }//n

        public RsaPublicKey(BigInteger publicExponent,BigInteger modulus)
        {
            this.Exponent = publicExponent;
            this.Modulus = modulus;
        }

        public void PrintConsole()
        {
            Console.WriteLine(new string('-',50));

            Console.WriteLine("Type: RSA public key");

            Console.WriteLine("Modulus(n):{0}({1} bits)", Modulus, BinaryConverter.GetBinaryLength(Modulus));
            Console.WriteLine("Exponent(e):{0}({1} bits)", Exponent, BinaryConverter.GetBinaryLength(Exponent));

            Console.WriteLine(new string('-', 50));
        }
    }
}
