using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits;
using AsymmetricCryptography.DataUnits.DigitalSignatures;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.RSA;

namespace AsymmetricCryptography.Core
{
    public sealed class RSA : AsymmetricAlgorithm,IEncryptor,ISignatutator
    {
        public RSA(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityVerificator, hashAlgorithm) { }

        public RSA(CryptographicHashAlgorithm hashAlgorithm = CryptographicHashAlgorithm.SHA_256) 
            : base(hashAlgorithm) { }

        public byte[] Encrypt(byte[] data, AsymmetricKey publicKey)
        {
            var key = publicKey as RsaPublicKey;

            if (key == null)
                throw new ArgumentException("Not RSA public key!");

            //получение отклытой экпоненты и модуля
            BigInteger exponent = key.PublicExponent;
            BigInteger modulus = key.Modulus;

            //вычисление размера блока
            int blockSize = BlockConverter.GetBlockSize(modulus);

            //перевод массива байтов в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(data, blockSize);

            //в список будут заноситься байты, полученные из зашифрованных блоков
            List<byte> encryptedBytes = new List<byte>();

            //каждый блок возводится в степень експоненты и берётся по модулю. переводится обратно в байты и запоминается
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = BigInteger.ModPow(blocks[i], exponent, modulus);

                encryptedBytes.AddRange(BlockConverter.BlockToBytes(blocks[i], blockSize + 1));
            }

            //в результате получается массив байтов зашифрованных данных
            return encryptedBytes.ToArray();
        }

        public byte[] Decrypt(byte[] encryptedData, AsymmetricKey privateKey)
        {
            var key = privateKey as RsaPrivateKey;

            if (key == null)
                throw new ArgumentException("Not RSA private key!");

            //получение закрытой экспоненты и модуля
            BigInteger exponent = key.PrivateExponent;
            BigInteger modulus = key.Modulus;

            //вычисление размера блоков
            int blockSize = BlockConverter.GetBlockSize(modulus);

            //перевод зашифрованных данных в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(encryptedData, blockSize + 1);

            //в список будет заноситься результат дешифровки
            List<byte> decryptedBytes = new List<byte>();

            //каждый блок возводится в степень закрытой экспоненты и берётся по модулю
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = BigInteger.ModPow(blocks[i], exponent, modulus);

                //в список добавляются расшифрованные байты
                decryptedBytes.AddRange(BlockConverter.BlockToBytes(blocks[i], blockSize));
            }

            // ниже идёт кастыль
            while (decryptedBytes[0] == 0)
                decryptedBytes.RemoveAt(0);

            if (decryptedBytes.Count % 2 != 0)
                decryptedBytes.Insert(0, 0);

            return decryptedBytes.ToArray();
        }

        public DigitalSignature CreateSignature(byte[] data, AsymmetricKey privateKey)
        {
            var key = privateKey as RsaPrivateKey;

            if (key == null)
                throw new ArgumentException("Not RSA private key!");

            BigInteger digest = new BigInteger(HashAlgorithm.GetHash(data));

            //если модуль ключа не может вместить хеш или хеш будет меньше нуля, то будут проблемы
            //поэтому хеш берётся по модулю
            digest = ModularArithmetic.Modulus(digest, key.Modulus);

            BigInteger signature = BigInteger.ModPow(digest, key.PrivateExponent, key.Modulus);

            return new RsaDigitalSignature(signature);
        }

        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, AsymmetricKey publicKey)
        {
            var key = publicKey as RsaPublicKey;
            var digitalSignature = signature as RsaDigitalSignature;

            if (key == null)
                throw new ArgumentException("Not RSA public key!");

            if (digitalSignature == null)
                throw new ArgumentException("Not RSA digital signature");

            BigInteger realHash = new BigInteger(HashAlgorithm.GetHash(data));

            //взятие хеша по модулю, аналогично того же, что и в создании подписи
            realHash = ModularArithmetic.Modulus(realHash, key.Modulus);

            BigInteger signatureHash = BigInteger.ModPow(digitalSignature.SignValue, key.PublicExponent, key.Modulus);

            return realHash == signatureHash;
        }
    }
}
