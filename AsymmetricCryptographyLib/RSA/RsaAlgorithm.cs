using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    public sealed class RsaAlgorithm: AsymmetricAlgorithm, IEncryptor, IDigitalSignatutator
    {
        public RsaPrivateKey PrivateKey
        {
            get
            {
                return base.privateKey as RsaPrivateKey;
            }

            set
            {
                if (value is RsaPrivateKey)
                    base.privateKey = value;
            }
        }

        public RsaPublicKey PublicKey
        {
            get
            {
                return base.publicKey as RsaPublicKey;
            }

            set
            {
                if (value is RsaPublicKey)
                    base.publicKey = value;
            }
        }

        //шифрование RSA с помощью открытого ключа
        public byte[] Encrypt(byte[] data,AsymmetricKey publicKey)
        {
            PublicKey = publicKey as RsaPublicKey;

            //получение отклытой экпоненты и модуля
            BigInteger exponent = PublicKey.PublicExponent;
            BigInteger modulus = PublicKey.Modulus;

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

        //расшифровка RSA с помощью закрытого ключа
        public byte[] Decrypt(byte[] encryptedData, AsymmetricKey privateKey)
        {
            PrivateKey = privateKey as RsaPrivateKey;

            //получение закрытой экспоненты и модуля
            BigInteger exponent = PrivateKey.PrivateExponent;
            BigInteger modulus = PrivateKey.Modulus;

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

            //decryptedBytes.RemoveAll(x => x == 0);

            while (decryptedBytes[0] == 0)
                decryptedBytes.RemoveAt(0);

            if (decryptedBytes.Count % 2 != 0)
                decryptedBytes.Insert(0, 0);

            return decryptedBytes.ToArray();
        }

        //создание цифровой подписи RSA путём возведения хеша в степень закрытой экспоненты
        public DigitalSignature CreateSignature(byte[] data,AsymmetricKey privateKey)
        {
            PrivateKey = privateKey as RsaPrivateKey;

            BigInteger digest = new BigInteger(HashAlgorithm.GetHash(data));

            //если модуль ключа не может вместить хеш или хеш будет меньше нуля, то будут проблемы
            //поэтому хеш берётся по модулю
            digest = ModularArithmetic.Modulus(digest, PrivateKey.Modulus);

            BigInteger signature = BigInteger.ModPow(digest, PrivateKey.PrivateExponent, PrivateKey.Modulus);

            return new RsaDigitalSignature(signature);
        }

        //проверка цифровой подписи RSA с помощью возведения подписи в степень открытой экспоненты и сравнением с хешем
        public bool VerifyDigitalSignature(DigitalSignature signature,byte[] data,AsymmetricKey publicKey)
        {
            RsaDigitalSignature digitalSignature = (RsaDigitalSignature)signature;

            PublicKey = publicKey as RsaPublicKey;

            BigInteger realHash = new BigInteger(HashAlgorithm.GetHash(data));

            //взятие хеша по модулю, аналогично того же, что и в создании подписи
            realHash = ModularArithmetic.Modulus(realHash, PublicKey.Modulus);

            BigInteger signatureHash = BigInteger.ModPow(digitalSignature.signValue, PublicKey.PublicExponent, PublicKey.Modulus);

            return realHash == signatureHash;
        }
    }
}
