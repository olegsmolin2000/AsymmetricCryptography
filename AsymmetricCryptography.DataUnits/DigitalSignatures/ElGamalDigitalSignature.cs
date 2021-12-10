namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    public sealed class ElGamalDigitalSignature:DigitalSignature
    {
        public BigInteger R { get; init; }
        public BigInteger S { get; init; }

        public ElGamalDigitalSignature(BigInteger r, BigInteger s)
        {
            R = r;
            S = s;
        }
    }
}
