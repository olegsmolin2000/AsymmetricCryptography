namespace AsymmetricCryptography.DataUnits.DigitalSignatures
{
    /// <summary>
    /// Digital signature data unit, contains signature values
    /// </summary>
    public abstract class DigitalSignature
    {
        public abstract void Accept(IDigitalSignatureVisitor visitor);
    }
}
