using AsymmetricCryptography.RandomNumberGenerators;
using System;
using System.Numerics;

namespace AsymmetricCryptography
{
    public abstract class NumberGenerator
    {
        protected static Random rand = new Random();

        protected PrimalityVerificator primalityVerificator;

        protected NumberGenerator(PrimalityVerificator primalityVerificator)
        {
            this.primalityVerificator = primalityVerificator;
        }

        public abstract BigInteger GenerateNumber(int binarySize);
        public abstract BigInteger GenerateNumber(BigInteger min,BigInteger max);

        public abstract BigInteger GeneratePrimeNumber(int binarySize);
        public abstract BigInteger GeneratePrimeNumber(BigInteger min,BigInteger max);

        public abstract override string ToString();
    }
}
