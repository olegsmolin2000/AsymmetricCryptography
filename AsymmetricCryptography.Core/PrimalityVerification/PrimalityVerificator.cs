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
    }

    /// <summary>
    /// Supported primality verificators enum
    /// </summary>
    public enum PrimalityVerifiator
    {
        MillerRabin
    }
}
