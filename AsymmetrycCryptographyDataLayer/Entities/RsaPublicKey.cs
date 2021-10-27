using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetrycCryptographyDataLayer.Entities
{
    class RsaPublicKey : AsymmetricKey
    {
        public override string AlgorithmName => "RSA";
        public override string Permittion => "Public";

        public BigInteger Exponent { get; }//e
        public BigInteger Modulus { get; }//n
    }
}
