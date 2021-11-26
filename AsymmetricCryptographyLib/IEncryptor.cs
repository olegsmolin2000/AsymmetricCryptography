using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public interface IEncryptor
    {
        public byte[] Encrypt(byte[] data, AsymmetricKey publicKey);
        public byte[] Decrypt(byte[] encryptedData, AsymmetricKey privateKey);
    }
}
