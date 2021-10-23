using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    abstract class AsymmetricAlgorithm
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;

        protected AsymmetricAlgorithm(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator)
        {
            this.numberGenerator = numberGenerator;
            this.primalityVerificator = primalityVerificator;
        }

        public abstract string AlgorithmName { get; }

        protected AsymmetricKey PrivateKey;
        protected AsymmetricKey PublicKey;

        public void PrintKeys()
        {
            PrivateKey.PrintConsole();
            PublicKey.PrintConsole();
        }


    }
}
