using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public interface IDigitalSignatutator
    {
        public DigitalSignature CreateSignature(byte[] data,AsymmetricKey privateKey);
        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data,AsymmetricKey publicKey);
    }
}
