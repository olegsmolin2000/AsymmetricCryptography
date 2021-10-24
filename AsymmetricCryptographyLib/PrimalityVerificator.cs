using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    public abstract class PrimalityVerificator
    {
        protected NumberGenerator numberGenerator;

        public void SetNumberGenerator(NumberGenerator numberGenerator)
        {
            this.numberGenerator = numberGenerator;
        }

        public abstract bool IsPrimal(BigInteger number, int k = 0);

        public bool IsCoprime(BigInteger num1, BigInteger num2)
        {
            while (num1 != 0 && num2 != 0)
            {
                if (num1 > num2)
                    num1 %= num2;
                else
                    num2 %= num1;
            }

            return BigInteger.Max(num1, num2) == 1;
        }
    }
}
