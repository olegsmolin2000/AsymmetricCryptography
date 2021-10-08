using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    abstract class AsymmetricAlgorithm
    {
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
