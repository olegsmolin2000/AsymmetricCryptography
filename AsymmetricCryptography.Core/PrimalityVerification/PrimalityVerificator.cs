using AsymmetricCryptography.Core.NumbersGenerating;

namespace AsymmetricCryptography.Core.PrimalityVerification
{
    /// <summary>
    /// Represents the base class from which all implementations of primality verificators must derived
    /// </summary>
    public abstract class PrimalityVerificator
    {
        /// <summary>
        /// Number generator used in some primality tests
        /// </summary>
        protected static readonly NumberGenerator NumberGenerator = new FibonacciNumberGenerator(null);

        /// <summary>
        /// Indicate whether the value is prime number
        /// </summary>
        /// <param name="number">Number for test</param>
        /// <returns><see langword="true"/> if number is prime, <see langword="false"/> otherwise</returns>
        public abstract bool IsPrime(BigInteger number);

        /// <summary>
        /// Indicate whether 2 numbers are coprime
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns><see langword="true"/> if numbers are coprime, <see langword="false"/> otherwise</returns>
        public static bool IsCoprime(BigInteger number1,BigInteger number2)
        {
            while (number1 != 0 && number2 != 0)
            {
                if (number1 > number2)
                    number1 %= number2;
                else
                    number2 %= number1;
            }

            return BigInteger.Max(number1, number2) == 1;
        }
    }

    /// <summary>
    /// Supported primality verificators enum
    /// </summary>
    public enum PrimalityVerifiator
    {
        MillerRabin
    }
}
