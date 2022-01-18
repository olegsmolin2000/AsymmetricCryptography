using AsymmetricCryptography.DataUnits;

namespace AsymmetricCryptography.Core.PrimalityVerificators
{
    /// <summary>
    /// Miller rabin probabilistic primality test
    /// </summary>
    public sealed class MillerRabinPrimalityVerificator : PrimalityVerificator
    {
        /// <summary>
        /// Count of rounds in testing
        /// </summary>
        public int K { get; set; } = 100;

        public MillerRabinPrimalityVerificator()
            :base(PrimalityTest.MillerRabin) { }

        public override bool IsPrime(BigInteger number)
        {
            if (number == 2 || number == 3)
                return true;

            if (number % 2 == 0 || number == 1 || number == 0)
                return false;

            BigInteger t = number - 1;

            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            for (int i = 0; i < K; i++)
            {
                BigInteger a = NumberGenerator.GenerateNumber(2, number - 2);

                BigInteger x = BigInteger.ModPow(a, t, number);

                if (x == 1 || x == number - 1)
                    continue;

                for (int r = 0; r < s - 1; r++)
                {
                    x = BigInteger.ModPow(x, 2, number);

                    if (x == 1)
                        return false;

                    if (x == number - 1)
                        break;
                }

                if (x != number - 1)
                    return false;
            }

            return true;
        }
    }
}
