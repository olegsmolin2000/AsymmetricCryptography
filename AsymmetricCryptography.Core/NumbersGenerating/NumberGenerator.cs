using AsymmetricCryptography.Core.PrimalityVerification;

namespace AsymmetricCryptography.Core.NumbersGenerating
{
    /// <summary>
    /// Represents the base class from which all implementations of number generators must derived
    /// </summary>
    public abstract class NumberGenerator
    {
        protected static readonly Random Rand = new Random();

        /// <summary>
        /// Primality verificator used in prime number generating, nullable
        /// </summary>
        protected PrimalityVerificator PrimalityVerificator { get; private init; }

        /// <summary>
        /// Initializes a new instance of the NumberGenerator, using the PrimalityVerificator
        /// </summary>
        /// <param name="primalityVerificator">Used primality verificator or null</param>
        protected NumberGenerator(PrimalityVerificator primalityVerificator = null)
        {
            PrimalityVerificator = primalityVerificator;
        }

        /// <summary>
        /// Generate a random BigInteger number
        /// </summary>
        /// <param name="binarySize">Count of bits in binary presentation of generated number</param>
        /// <returns>Random BigInteger number</returns>
        public abstract BigInteger GenerateNumber(int binarySize);

        /// <summary>
        /// Generate a random BigInteger number that is within a specified range
        /// </summary>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Upper bound</param>
        /// <returns>Random BigInteger number in specified range</returns>
        public BigInteger GenerateNumber(BigInteger min, BigInteger max)
        {
            return 0;
        }

        /// <summary>
        /// Generate a random BigInteger prime number
        /// </summary>
        /// <param name="binarySize">Count of bits in binary presentation of generated number</param>
        /// <returns>Random BigInteger prime number</returns>
        public BigInteger GeneratePrimeNumber(int binarySize)
        {
            return 0;
        }

        /// <summary>
        /// Generate a random BigInteger prime number that is within a specified range
        /// </summary>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Upper bound</param>
        /// <returns>Random BigInteger prime number in specified range</returns>
        public BigInteger GeneratePrimeNumber(BigInteger min, BigInteger max)
        {
            return 0;
        }
    }

    /// <summary>
    /// Supported number generators enum
    /// </summary>
    public enum NumberGemerator
    {
        Fibonacci,
        BlumBlumShub
    }
}
