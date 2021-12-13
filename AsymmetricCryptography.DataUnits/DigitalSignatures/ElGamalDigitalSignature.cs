namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    /// <summary>
    /// ElGamal scheme digital signature (R,S)
    /// </summary>
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
