namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    public sealed class RsaDigitalSignature : DigitalSignature
    {
        public BigInteger SignValue { get; init; }

        public RsaDigitalSignature(BigInteger signValue)
        {
            SignValue = signValue;
        }
    }
}
