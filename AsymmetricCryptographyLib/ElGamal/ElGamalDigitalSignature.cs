using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.ElGamal
{
    //цифровая подпись по схеме Эль Гамаля
    public class ElGamalDigitalSignature :DigitalSignature
    {
        public override string Type => "El Gamal scheme digital signature";

        public BigInteger R { get; }
        public BigInteger S { get; }

        public ElGamalDigitalSignature(BigInteger r, BigInteger s)
        {
            this.R = r;
            this.S = s;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("R: " + R + "\n");
            result.Append("S: " + S);

            return result.ToString();
        }
    }
}
