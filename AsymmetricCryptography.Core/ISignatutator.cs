using AsymmetricCryptography.DataUnits.DigitalSignatures;
using AsymmetricCryptography.DataUnits.Keys;

namespace AsymmetricCryptography.Core
{
    public interface ISignatutator
    {
        public DigitalSignature CreateSignature(byte[] data, AsymmetricKey privateKey);
        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, AsymmetricKey publicKey);
    }
}
