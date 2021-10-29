using System;
using System.Numerics;
using AsymmetricCryptography.CryptographicHash;

using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public abstract class AsymmetricAlgorithm
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;
        protected CryptographicHashAlgorithm hashAlgorithm;

        protected KeysGenerator keysGenerator;

        protected AsymmetricKey privateKey;
        protected AsymmetricKey publicKey;

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
    }
}
