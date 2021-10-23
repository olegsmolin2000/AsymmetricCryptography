using System;
using System.Collections.Generic;
using System.Text;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography
{
    abstract class AsymmetricAlgorithm
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;
        protected CryptographicHashAlgorithm hashAlgorithm;

        protected AsymmetricAlgorithm(Parameters parameters)
        {
            this.numberGenerator = parameters.numberGenerator;
            this.primalityVerificator = parameters.primalityVerificator;
            this.hashAlgorithm = parameters.hashAlgorithm;
        }

        public abstract string AlgorithmName { get; }

        protected AsymmetricKey PrivateKey;
        protected AsymmetricKey PublicKey;

        public void PrintKeys()
        {
            PrivateKey.PrintConsole();
            Console.WriteLine();
            PublicKey.PrintConsole();
        }


    }
}
