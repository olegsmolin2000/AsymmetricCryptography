namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    /// <summary>
    /// RSA digital signature, contains only BigInteger sign value
    /// </summary>
    public sealed class RsaDigitalSignature : DigitalSignature
    {
        public BigInteger SignValue { get; init; }

        public RsaDigitalSignature(BigInteger signValue)
        {
            SignValue = signValue;
        }
    }
}
