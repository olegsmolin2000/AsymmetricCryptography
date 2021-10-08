using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography.RSA
{
    //todo:
    // zero bytes incorrect encryption
    sealed class RsaAlgorithm: AsymmetricAlgorithm, IEncryptor,IDigitalSignature
    {
        public override string AlgorithmName => "RSA";

        private new RsaPublicKey PublicKey
        {
            get
            {
                return base.PublicKey as RsaPublicKey;
            }
            set
            {
                if (value is RsaPublicKey)
                    base.PublicKey = value;
            }
        }

        private new RsaPrivateKey PrivateKey
        {
            get
            {
                return base.PrivateKey as RsaPrivateKey;
            }
            set
            {
                if (value is RsaPrivateKey)
                    base.PrivateKey = value;
            }
        }

        public RsaAlgorithm(int keyBinarySize)
        {
            RsaPrivateKey rsaPrivateKey;
            RsaPublicKey rsaPublicKey;

            KeysGenerator.RsaKeysGeneration(keyBinarySize, out rsaPrivateKey, out rsaPublicKey);

            PublicKey = rsaPublicKey;
            PrivateKey = rsaPrivateKey;
        }

        public RsaAlgorithm(RsaPrivateKey privateKey,RsaPublicKey publicKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        //шифрование RSA с помощью открытого ключа
        public byte[] Encrypt(byte[] data)
        {
            //получение отклытой экпоненты и модуля
            BigInteger exponent = PublicKey.Exponent;
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
        public byte[] Decrypt(byte[] encryptedData)
        {
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
                decryptedBytes.AddRange(BlockConverter.BlockToBytes(blocks[i], blockSize + 1));
            }

            decryptedBytes.RemoveAll(x => x == 0);

            return decryptedBytes.ToArray();
        }

        //создание цифровой подписи RSA путём возведения хеша в степень закрытой экспоненты
        public DigitalSignature CreateSignature(byte[] data,CryptographicHashAlgorithm hashAlgorithm)
        {
            BigInteger digest = new BigInteger(hashAlgorithm.GetHash(data));

            //если модуль ключа не может вместить хеш или хеш будет меньше нуля, то будут проблемы
            //поэтому хеш берётся по модулю
            digest = ModularArithmetic.Modulus(digest, PrivateKey.Modulus);

            BigInteger signature = BigInteger.ModPow(digest, PrivateKey.PrivateExponent, PrivateKey.Modulus);

            return new RsaDigitalSignature(signature);
        }

        //проверка цифровой подписи RSA с помощью возведения подписи в степень открытой экспоненты и сравнением с хешем
        public bool VerifyDigitalSignature(DigitalSignature signature,byte[] data,CryptographicHashAlgorithm hashAlgorithm)
        {
            BigInteger realHash = new BigInteger(hashAlgorithm.GetHash(data));

            //взятие хеша по модулю, аналогично того же, что и в создании подписи
            realHash = ModularArithmetic.Modulus(realHash, PublicKey.Modulus);

            BigInteger sign = (signature as RsaDigitalSignature).signValue;

            BigInteger signatureHash = BigInteger.ModPow(sign, PublicKey.Exponent, PublicKey.Modulus);

            return realHash == signatureHash;
        }
    }
}
