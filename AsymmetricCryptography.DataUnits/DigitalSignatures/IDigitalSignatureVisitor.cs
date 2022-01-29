namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    public interface IDigitalSignatureVisitor
    {
        public void VisitRsaDigitalSignature(RsaDigitalSignature rsaDigitalSignature);
        public void VisitElGamalDigitalSignature(ElGamalDigitalSignature elGamalDigitalSignature);
    }
}
