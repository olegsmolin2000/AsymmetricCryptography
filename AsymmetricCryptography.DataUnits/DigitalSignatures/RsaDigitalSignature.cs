namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    /// <summary>
    /// RSA digital signature, contains only BigInteger sign value
    /// </summary>
    public sealed class RsaDigitalSignature : DigitalSignature
    {
        public BigInteger SignValue { get; set; }

        public RsaDigitalSignature(BigInteger signValue)
        {
            SignValue = signValue;
        }

        public override void Accept(IDigitalSignatureVisitor visitor)
        {
            visitor.VisitRsaDigitalSignature(this);
        }
    }
}
