using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    public class RsaDigitalSignature : DigitalSignature
    {
        public override string Type => "RSA digital signature";

        public BigInteger signValue { get; }

        public RsaDigitalSignature(BigInteger signValue)
        {
            this.signValue = signValue;
        }
        public override string ToString()
        {
            return new string("Sign value: " + signValue);
        }
    }
}
