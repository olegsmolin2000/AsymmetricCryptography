using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography
{
    public abstract class AsymmetricAlgorithm
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;
        protected CryptographicHashAlgorithm hashAlgorithm;

        protected KeysGenerator keysGenerator;

        public abstract string AlgorithmName { get; }

        protected AsymmetricKey PrivateKey;
        protected AsymmetricKey PublicKey;

        protected AsymmetricAlgorithm(Parameters parameters)
        {
            this.numberGenerator = parameters.numberGenerator;
            this.primalityVerificator = parameters.primalityVerificator;
            this.hashAlgorithm = parameters.hashAlgorithm;
        }

        public void SetKeys(AsymmetricKey privateKey, AsymmetricKey publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public void SetKeysGenerator(KeysGenerator keysGenerator)
        {
            this.keysGenerator = keysGenerator;
        }

        public void GenerateKeys(int binarySize)
        {
            keysGenerator.GenerateKeyPair(binarySize, out PrivateKey, out PublicKey);
        }

        // выбор сессионного ключа k. случайное целое число, взаимно простое с (p - 1), 1 < k < p - 1
        protected BigInteger GenerateSessionKey(BigInteger p)
        {
            BigInteger sessionKey = 0;

            do
            {
                sessionKey = numberGenerator.GenerateNumber(2, p - 2);
            } while (!primalityVerificator.IsCoprime(sessionKey, p - 1));

            return sessionKey;
        }

        public void PrintKeys()
        {
            PrivateKey.PrintConsole();
            Console.WriteLine();
            PublicKey.PrintConsole();
        }
    }
}
