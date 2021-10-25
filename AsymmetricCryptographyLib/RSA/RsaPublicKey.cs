using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    public class RsaPublicKey : AsymmetricKey
    {
        public BigInteger Exponent { get; }//e
        public BigInteger Modulus { get; }//n

        public override string AlgorithmName => "RSA";
        public override string Permittion => "Public";


        public RsaPublicKey(BigInteger publicExponent,BigInteger modulus)
        {
            this.Exponent = publicExponent;
            this.Modulus = modulus;
        }

        public override void PrintConsole()
        {
            Console.WriteLine(new string('-',50));

            Console.WriteLine("Type: RSA public key\n");

            Console.WriteLine("Modulus(n):{0}({1} bits)\n", Modulus, BinaryConverter.GetBinaryLength(Modulus));
            Console.WriteLine("Exponent(e):{0}({1} bits)", Exponent, BinaryConverter.GetBinaryLength(Exponent));

            Console.WriteLine(new string('-', 50));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Modulus(n):" + Modulus + " (" + BinaryConverter.GetBinaryLength(Modulus) + " bits)\n");
            result.Append("Exponent(e):" + Exponent + " (" + BinaryConverter.GetBinaryLength(Exponent) + " bits)\n");

            return result.ToString();
        }
    }
}
