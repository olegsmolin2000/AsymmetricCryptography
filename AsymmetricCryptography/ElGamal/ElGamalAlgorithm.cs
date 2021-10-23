﻿using AsymmetricCryptography.CryptographicHash;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.ElGamal
{
    class ElGamalAlgorithm : AsymmetricAlgorithm, IEncryptor, IDigitalSignatutator
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
            //получение параметров
            BigInteger p = PublicKey.Parameters.P;
            BigInteger g = PublicKey.Parameters.G;

            //получение открытого ключа
            BigInteger y = PublicKey.Y;

            //вычисление размера блока
            int blockSize = BlockConverter.GetBlockSize(p);

            //перевод массива байтов в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(data, blockSize);

            //в список будут заноситься байты, полученные из зашифрованных блоков
            List<byte> encryptedBytes = new List<byte>();

            //шифрование
            for (int i = 0; i < blocks.Length; i++)
            {
                // выбирается сессионный ключ k. случайное целое число, взаимно простое с (p - 1)
                // 1 < k < p - 1
                BigInteger k = GenerateSessionKey(p);

                // a = g^k mod p
                BigInteger a = BigInteger.ModPow(g, k, p);
                // b = (y^x * M) mod p
                BigInteger b = ModularArithmetic.Modulus(BigInteger.ModPow(y, k, p) * blocks[i], p);

                // пара (a,b) - шифротекст
                encryptedBytes.AddRange(BlockConverter.BlockToBytes(a, blockSize + 1));
                encryptedBytes.AddRange(BlockConverter.BlockToBytes(b, blockSize + 1));
            }

            //в результате получается массив байтов зашифрованных данных
            return encryptedBytes.ToArray();
        }

        public byte[] Decrypt(byte[] encryptedData)
        {
            // получение параметров
            BigInteger p = PrivateKey.Parameters.P;
            BigInteger g = PrivateKey.Parameters.G;

            // получение закрытого ключа
            BigInteger x = PrivateKey.X;

            //вычисление размера блоков
            int blockSize = BlockConverter.GetBlockSize(p);

            //перевод зашифрованных данных в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(encryptedData, blockSize + 1);

            //в список будет заноситься результат дешифровки
            List<byte> decryptedBytes = new List<byte>();

            // дешифровка
            for (int i = 0; i < blocks.Length; i+=2)
            {
                BigInteger a = blocks[i];
                BigInteger b = blocks[i + 1];

                // M = (b * (a^x)^-1) mod p
                BigInteger decryption = ModularArithmetic.Modulus((b * ModularArithmetic.GetMultiplicativeModuloReverse(BigInteger.ModPow(a, x, p), p)), p);

                //в список добавляются расшифрованные байты
                decryptedBytes.AddRange(BlockConverter.BlockToBytes(decryption, blockSize + 1));
            }

            decryptedBytes.RemoveAll(x => x == 0);

            return decryptedBytes.ToArray();
        }

        public DigitalSignature CreateSignature(byte[] data, CryptographicHashAlgorithm hashAlgorithm)
        {
            //вычисление хеша по криптографической хеш функции
            BigInteger hash = new BigInteger(hashAlgorithm.GetHash(data));

            //получение параметров
            BigInteger p = PrivateKey.Parameters.P;
            BigInteger g = PrivateKey.Parameters.G;

            //хеш берётся по модулю p - 1, чтобы не было проблем с ним 
            hash = ModularArithmetic.Modulus(hash, p - 1);

            //выбор сессионного ключа
            BigInteger k = GenerateSessionKey(p);

            // r = g^x mod p
            BigInteger r = BigInteger.ModPow(g, k, p);

            //s = ((H - x * r) * k^-1) mod (p - 1)
            BigInteger reverseK = ModularArithmetic.GetMultiplicativeModuloReverse(k, p - 1);
            hash -= PrivateKey.X * r;

            BigInteger s = ModularArithmetic.Modulus(hash * reverseK, p - 1);

            //подписью является пара (r, s)
            return new ElGamalDigitalSignature(r, s);
        }

        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, CryptographicHashAlgorithm hashAlgorithm)
        {
            ElGamalDigitalSignature digitalSignature = (ElGamalDigitalSignature)signature;

            //получение значений подписи
            BigInteger r = digitalSignature.R;
            BigInteger s = digitalSignature.S;

            //получение параметров
            BigInteger p = PublicKey.Parameters.P;
            BigInteger g = PublicKey.Parameters.G;

            //если условия ниже не выполняются то подпись точно неверна
            if (r <= 0 || r >= p)
                return false;

            if (s <= 0 || s >= p - 1)
                return false;

            //вычисление хеша по криптографической хеш функции
            BigInteger hash = new BigInteger(hashAlgorithm.GetHash(data));

            //хеш берётся по модулю p - 1, чтобы не было проблем с ним 
            hash = ModularArithmetic.Modulus(hash, p - 1);

            BigInteger y = PublicKey.Y;

            //вычисление левой части (y^r * r^s) mod p
            BigInteger left = ModularArithmetic.Modulus(BigInteger.ModPow(y, r, p) * BigInteger.ModPow(r, s, p), p);

            //вычисление правой части g^m mod p
            BigInteger right = BigInteger.ModPow(g, hash, p);

            //если левая и правая части равны то подпись верна
            return left == right;
        }

        // выбор сессионного ключа k. случайное целое число, взаимно простое с (p - 1)
        // 1 < k < p - 1
        private BigInteger GenerateSessionKey(BigInteger p)
        {
            BigInteger sessionKey = 0;

            do
            {
                sessionKey = FibonacciNumberGenerator.GenerateNumber(2, p - 2);
            } while (!MillerRabinPrimalityVerificator.IsCoprime(sessionKey, p - 1));

            return sessionKey;
        }
    }
}
