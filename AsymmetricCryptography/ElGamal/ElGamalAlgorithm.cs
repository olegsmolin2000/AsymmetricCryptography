using AsymmetricCryptography.CryptographicHash;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.ElGamal
{
    class ElGamalAlgorithm : AsymmetricAlgorithm, IEncryptor, IDigitalSignature
    {
        public override string AlgorithmName => "El Gamal";

        private new ElGamalPublicKey PublicKey
        {
            get
            {
                return base.PublicKey as ElGamalPublicKey;
            }
            set
            {
                if (value is ElGamalPublicKey)
                    base.PublicKey = value;
            }
        }

        private new ElGamalPrivateKey PrivateKey
        {
            get
            {
                return base.PrivateKey as ElGamalPrivateKey;
            }
            set
            {
                if (value is ElGamalPrivateKey)
                    base.PrivateKey = value;
            }
        }

        public ElGamalAlgorithm(ElGamalPrivateKey privateKey,ElGamalPublicKey publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public byte[] Encrypt(byte[] data)
        {
            BigInteger p = PublicKey.Parameters.P;
            BigInteger g = PublicKey.Parameters.G;

            BigInteger y = PublicKey.Y;

            //вычисление размера блока
            int blockSize = BlockConverter.GetBlockSize(p);

            //перевод массива байтов в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(data, blockSize);

            //в список будут заноситься байты, полученные из зашифрованных блоков
            List<byte> encryptedBytes = new List<byte>();

            //
            for (int i = 0; i < blocks.Length; i++)
            {
                BigInteger k = GenerateSessionKey(p);

                BigInteger a = BigInteger.ModPow(g, k, p);
                BigInteger b = ModularArithmetic.Modulus(BigInteger.ModPow(y, k, p) * blocks[i], p);

                encryptedBytes.AddRange(BlockConverter.BlockToBytes(a, blockSize + 1));
                encryptedBytes.AddRange(BlockConverter.BlockToBytes(b, blockSize + 1));
            }

            //в результате получается массив байтов зашифрованных данных
            return encryptedBytes.ToArray();
        }

        public byte[] Decrypt(byte[] encryptedData)
        {
            BigInteger p = PrivateKey.Parameters.P;
            BigInteger g = PrivateKey.Parameters.G;

            BigInteger x = PrivateKey.X;

            //вычисление размера блоков
            int blockSize = BlockConverter.GetBlockSize(p);

            //перевод зашифрованных данных в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(encryptedData, blockSize + 1);

            //в список будет заноситься результат дешифровки
            List<byte> decryptedBytes = new List<byte>();

            //каждый блок возводится в степень закрытой экспоненты и берётся по модулю
            for (int i = 0; i < blocks.Length; i+=2)
            {
                BigInteger a = blocks[i];
                BigInteger b = blocks[i + 1];

                BigInteger decryption = ModularArithmetic.Modulus((b * ModularArithmetic.GetMultiplicativeModuloReverse(BigInteger.ModPow(a, x, p), p)), p);

                //в список добавляются расшифрованные байты
                decryptedBytes.AddRange(BlockConverter.BlockToBytes(decryption, blockSize + 1));
            }

            decryptedBytes.RemoveAll(x => x == 0);

            return decryptedBytes.ToArray();
        }

        public DigitalSignature CreateSignature(byte[] data, CryptographicHashAlgorithm hashAlgorithm)
        {
            throw new NotImplementedException();
        }

        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, CryptographicHashAlgorithm hashAlgorithm)
        {
            throw new NotImplementedException();
        }

        private BigInteger GenerateSessionKey(BigInteger p)
        {
            BigInteger sessionKey = 0;

            do
            {
                sessionKey = NumberGenerator.GenerateNumber(2, p - 2);
            } while (!PrimalityVerifications.IsCoprime(sessionKey, p - 1));

            return sessionKey;
        }
    }
}
