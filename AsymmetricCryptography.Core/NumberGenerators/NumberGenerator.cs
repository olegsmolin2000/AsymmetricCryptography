using AsymmetricCryptography.Core.PrimalityVerificators;

namespace AsymmetricCryptography.Core.NumberGenerators
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
        protected PrimalityVerificator PrimalityVerificator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the NumberGenerator, using the PrimalityVerificator
        /// </summary>
        /// <param name="primalityVerificator">Used primality verificator or null</param>
        protected NumberGenerator(PrimalityVerificator primalityVerificator = null!)
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
            if (min > max)
                throw new ArgumentException("Min > max");

            int minBitLength = Convert.ToInt32(min.GetBitLength());
            int maxBitLength = Convert.ToInt32(max.GetBitLength());

            BigInteger number;

            do
            {
                int randomBinarySize = Rand.Next(minBitLength, maxBitLength + 1);

                number = GenerateNumber(randomBinarySize);
            } while (number < min || number > max);

            return number;
        }

        /// <summary>
        /// Generate a random BigInteger prime number
        /// </summary>
        /// <param name="binarySize">Count of bits in binary presentation of generated number</param>
        /// <returns>Random BigInteger prime number</returns>
        public BigInteger GeneratePrimeNumber(int binarySize)
        {
            if(PrimalityVerificator==null)
                PrimalityVerificator=new MillerRabinPrimalityVerificator();

            BigInteger number = 0;

            do
            {
                number = GenerateNumber(binarySize);
            } while (!PrimalityVerificator.IsPrime(number));

            return number;
        }

        /// <summary>
        /// Generate a random BigInteger prime number that is within a specified range
        /// </summary>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Upper bound</param>
        /// <returns>Random BigInteger prime number in specified range</returns>
        public BigInteger GeneratePrimeNumber(BigInteger min, BigInteger max)
        {
            if (PrimalityVerificator == null)
                PrimalityVerificator = new MillerRabinPrimalityVerificator();

            BigInteger number = 0;

            do
            {
                number = GenerateNumber(min, max);
            } while (!PrimalityVerificator.IsPrime(number));

            return number;
        }

        /// <summary>
        /// Convert number generator enum to instance of specified value
        /// </summary>
        /// <param name="numberGemerator"></param>
        /// <returns>Insance of number generator</returns>
        /// <exception cref="ArgumentException"></exception>
        public static NumberGenerator Parse(RandomNumberGenerator numberGenerator) =>
            numberGenerator switch
            {
                RandomNumberGenerator.Fibonacci => new FibonacciNumberGenerator(),
                _ => throw new ArgumentException("Invalid enum value")
            };
    }

    /// <summary>
    /// Supported number generators enum
    /// </summary>
    public enum RandomNumberGenerator
    {
        Fibonacci
        //BlumBlumShub
    }
}
