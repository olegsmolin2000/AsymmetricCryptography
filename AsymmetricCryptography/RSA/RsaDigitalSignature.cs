using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    class RsaDigitalSignature : DigitalSignature
    {
        public override string Type => "RSA digital signature";

        public BigInteger signValue { get; }

        public RsaDigitalSignature(BigInteger signValue)
        {
            this.signValue = signValue;
        }

        public override void PrintConsole()
        {
            Console.WriteLine(Type + "\nSign value:" + signValue);
        }
    }
}
