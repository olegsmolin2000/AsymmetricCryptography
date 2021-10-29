﻿using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public abstract class KeysGenerator
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;
        protected CryptographicHashAlgorithm hashAlgorithm;

        protected KeysGenerator(Parameters parameters)
        {
            this.numberGenerator = parameters.numberGenerator;
            this.primalityVerificator = parameters.primalityVerificator;
            this.hashAlgorithm = parameters.hashAlgorithm;
        }

        public abstract void GenerateKeyPair(string name, int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey); 
    }
}
