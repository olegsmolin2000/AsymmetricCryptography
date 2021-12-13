using AsymmetricCryptography.Core.PrimalityVerification;

namespace AsymmetricCryptography.Core.NumbersGenerating
{
    public abstract class NumberGenerator
    {
        protected static readonly Random Rand = new Random();

        public abstract BigInteger GenerateNumber(int binarySize);

        public BigInteger GenerateNumber(BigInteger min, BigInteger max)
        {
            return 0;
        }

        public BigInteger GeneratePrimeNumber(int binarySize)
        {
            return 0;
        }

        public BigInteger GeneratePrimeNumber(BigInteger min, BigInteger max)
        {
            return 0;
        }
    }

    public enum NumberGemerator
    {
        Fibonacci,
        BlumBlumShub
    }
}
