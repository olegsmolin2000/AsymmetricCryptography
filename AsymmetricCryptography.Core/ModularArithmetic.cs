namespace AsymmetricCryptography.Core
{
    /// <summary>
    /// Provides static methods for modular arithmetic, a little bit of number theory common mathematical functions
    /// </summary>
    internal static class ModularArithmetic
    {
        /// <summary>
        /// Performs modulus division on a number
        /// </summary>
        /// <param name="value">Number to divide</param>
        /// <param name="modulus">The number by which to divide value raised to the exponent power</param>
        /// <returns>The remainder after dividing value by modulus</returns>
        public static BigInteger Modulus(BigInteger value, BigInteger modulus)
        {
            value = value % modulus;

            if (value < 0)
                return value + modulus;
            else
                return value;
        }

        /// <summary>
        /// Compute modular multiplicative inverse
        /// </summary>
        /// <param name="number">Number to get multiplicative inverse</param>
        /// <param name="mod">Modulus</param>
        /// <returns></returns>
        public static BigInteger GetModularMultiplicativeInverse(BigInteger number, BigInteger mod)
        {
            BigInteger x, y;

            BigInteger result = GcdExtended(number, mod, out x, out y);

            return (x % mod + mod) % mod;
        }

        /// <summary>
        /// Compute primitive root by modulus
        /// </summary>
        /// <param name="modulus">Modulu to find his primitive root</param>
        /// <returns>Primitive root</returns>
        public static BigInteger GetPrimitiveRoot(BigInteger modulus)
        {
            List<BigInteger> fact = new List<BigInteger>();
            
            BigInteger phi = modulus - 1, n = phi;

            for (BigInteger i = 2; i * i <= n; ++i)
                if (n % i == 0)
                {
                    fact.Add(i);
                    while (n % i == 0)
                        n /= i;
                }

            if (n > 1)
                fact.Add(n);

            for (BigInteger res = 2; res <= modulus; ++res)
            {
                bool ok = true;
                for (int i = 0; i < fact.Count && ok; ++i)
                    ok &= BigInteger.ModPow(res, phi / fact[i], modulus) != 1;
                if (ok) return res;
            }
            return -1;
        }

        /// <summary>
        /// Extended Euclidean algorithm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static BigInteger GcdExtended(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 1;
                y = 1;

                return b;
            }

            BigInteger x1, y1;

            BigInteger gcd = GcdExtended(b % a, a, out x1, out y1);

            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }
    }
}
