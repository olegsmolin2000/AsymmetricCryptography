using AsymmetricCryptography.DataUnits.Keys;

namespace AsymmetricCryptography.Core
{
    public interface IEncryptor
    {
        public byte[] Encrypt(byte[] data, AsymmetricKey publicKey);
        public byte[] Decrypt(byte[] encryptedData, AsymmetricKey privateKey);
    }
}
