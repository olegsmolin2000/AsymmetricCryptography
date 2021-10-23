using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    abstract class KeysGenerator
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;

        protected KeysGenerator(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator)
        {
            this.numberGenerator = numberGenerator;
            this.primalityVerificator = primalityVerificator;
        }

        public abstract void GenerateKeyPair(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey); 
    }
}
