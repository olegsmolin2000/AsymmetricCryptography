using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography.RSA
{
    static class RsaAlgorithm
    {
        //шифрование RSA с помощью открытого ключа
        public static byte[] Encrypt(byte[] data,RsaPublicKey publicKey)
        {
            //получение отклытой экпоненты и модуля
            BigInteger exponent = publicKey.Exponent;
            BigInteger modulus = publicKey.Modulus;

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
        public static byte[] Decryption(byte[] encryptedData,RsaPrivateKey privateKey)
        {
            //получение закрытой экспоненты и модуля
            BigInteger exponent = privateKey.PrivateExponent;
            BigInteger modulus = privateKey.Modulus;

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
        public static BigInteger SignatureCreating(byte[] message,RsaPrivateKey privateKey,CryptographicHashAlgorithm hashAlgorithm)
        {
            BigInteger digest = new BigInteger(hashAlgorithm.GetHash(message));

            //если модуль ключа не может вместить хеш или хеш будет меньше нуля, то будут проблемы
            //поэтому хеш берётся по модулю
            digest = ModularArithmetic.Modulus(digest, privateKey.Modulus);

            BigInteger signature = BigInteger.ModPow(digest, privateKey.PrivateExponent, privateKey.Modulus);

            return signature;
        }

        //проверка цифровой подписи RSA с помощью возведения подписи в степень открытой экспоненты и сравнением с хешем
        public static bool SignatureVerification(BigInteger signature,byte[] message,RsaPublicKey publicKey,CryptographicHashAlgorithm hashAlgorithm)
        {
            BigInteger realHash = new BigInteger(hashAlgorithm.GetHash(message));

            //взятие хеша по модулю, аналогично того же, что и в создании подписи
            realHash = ModularArithmetic.Modulus(realHash, publicKey.Modulus);

            BigInteger signatureHash = BigInteger.ModPow(signature, publicKey.Exponent, publicKey.Modulus);

            return realHash == signatureHash;
        }
    }
}
