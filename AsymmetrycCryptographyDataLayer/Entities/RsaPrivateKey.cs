using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetrycCryptographyDataLayer.Entities
{
    class RsaPrivateKey : AsymmetricKey
    {
        public override string AlgorithmName => "RSA";
        public override string Permittion => "Private";

        public BigInteger Modulus { get; }//n
        public BigInteger PublicExponent { get; }//e
        public BigInteger PrivateExponent { get; }//d
        public BigInteger Prime1 { get; }//p
        public BigInteger Prime2 { get; }//q
        public BigInteger Exponent1 { get; }//d mod(p-1)
        public BigInteger Exponent2 { get; }//d mod(q-1)
        public BigInteger Coefficient { get; }//(1/q) mod p
    }
}
