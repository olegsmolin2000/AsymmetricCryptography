using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    public class RsaPrivateKey :AsymmetricKey
    {
        public BigInteger Modulus { get; }//n
        public BigInteger PublicExponent { get; }//e
        public BigInteger PrivateExponent { get; }//d
        public BigInteger Prime1 { get; }//p
        public BigInteger Prime2 { get; }//q
        public BigInteger Exponent1 { get; }//d mod(p-1)
        public BigInteger Exponent2 { get; }//d mod(q-1)
        public BigInteger Coefficient { get; }//(1/q) mod p
        
        public override string AlgorithmName => "RSA";
        public override string Permittion => "Private";

        public RsaPrivateKey(BigInteger n, BigInteger e, BigInteger d, BigInteger p, BigInteger q)
        {
            this.Modulus = n;
            this.PublicExponent = e;
            this.PrivateExponent = d;
            this.Prime1 = p;
            this.Prime2 = q;

            Exponent1 = ModularArithmetic.Modulus(d, p - 1);
            Exponent2 = ModularArithmetic.Modulus(d, q - 1);

            Coefficient = ModularArithmetic.GetMultiplicativeModuloReverse(q, p);
        }

        public override void PrintConsole()
        {
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Type: RSA private key\n");

            Console.WriteLine("Modulus(n):{0}({1} bits)\n", Modulus, BinaryConverter.GetBinaryLength(Modulus));
            Console.WriteLine("PublicExponent(e):{0}({1} bits)\n", PublicExponent, BinaryConverter.GetBinaryLength(PublicExponent));
            Console.WriteLine("PrivateExponent(d):{0}({1} bits)\n", PrivateExponent, BinaryConverter.GetBinaryLength(PrivateExponent));
            Console.WriteLine("Prime1(p):{0}({1} bits)\n", Prime1, BinaryConverter.GetBinaryLength(Prime1));
            Console.WriteLine("Prime2(q):{0}({1} bits)\n", Prime2, BinaryConverter.GetBinaryLength(Prime2));
            Console.WriteLine("Exponent1(d mod(p-1)):{0}({1} bits)\n", Exponent1, BinaryConverter.GetBinaryLength(Exponent1));
            Console.WriteLine("Exponent2(d mod(q-1)):{0}({1} bits)\n", Exponent2, BinaryConverter.GetBinaryLength(Exponent2));
            Console.WriteLine("Coefficient((1/q) mod p):{0}({1} bits)", Coefficient, BinaryConverter.GetBinaryLength(Coefficient));

            Console.WriteLine(new string('-', 50));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Modulus(n):" + Modulus + " (" + BinaryConverter.GetBinaryLength(Modulus) + " bits)\n");
            result.Append("Exponent(d):" + PrivateExponent + " (" + BinaryConverter.GetBinaryLength(PrivateExponent) + " bits)\n");

            return result.ToString();
        }
    }
}
