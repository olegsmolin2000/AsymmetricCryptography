using AsymmetricCryptography.Core.PrimalityVerification;

namespace AsymmetricCryptography.Core.NumbersGenerating
{
    /// <summary>
    /// Lagged Fibonacci pseudorandom number generator
    /// </summary>
    public sealed class FibonacciNumberGenerator : NumberGenerator
    {
        /// <summary>
        /// List of most popular lagg pairs (a,b) or (j,k)
        /// </summary>
        private static readonly List<(int, int)> Laggs = new List<(int, int)>()
        {
            (24, 55), (38, 89), (37, 100), (30, 127),
            (83, 258), (107, 378), (273, 607), (1029, 2281),
            (576, 3217), (4187, 9689), (7083, 19937), (9739, 23209)
        };

        /// <summary>
        /// Initializes a new instance of the Lagged Fibonacci Number Generator, using the Primality Verificator
        /// </summary>
        /// <param name="primalityVerificator">Primality verificator used in prime numbers generating</param>
        public FibonacciNumberGenerator(PrimalityVerificator? primalityVerificator = null)
            :base(primalityVerificator) { }

        public override BigInteger GenerateNumber(int binarySize)
        {
            throw new NotImplementedException();
        }
    }
}
