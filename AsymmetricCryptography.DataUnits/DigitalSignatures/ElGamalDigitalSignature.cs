namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    /// <summary>
    /// ElGamal scheme digital signature (R,S)
    /// </summary>
    public sealed class ElGamalDigitalSignature:DigitalSignature
    {
        public BigInteger R { get; set; }
        public BigInteger S { get; set; }

        public ElGamalDigitalSignature(BigInteger r, BigInteger s)
        {
            R = r;
            S = s;
        }

        public override void Accept(IDigitalSignatureVisitor visitor)
        {
            visitor.VisitElGamalDigitalSignature(this);
        }
    }
}
