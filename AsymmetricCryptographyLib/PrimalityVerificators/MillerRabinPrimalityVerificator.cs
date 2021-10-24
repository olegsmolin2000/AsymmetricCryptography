using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.PrimalityVerificators
{
    public sealed class MillerRabinPrimalityVerificator: PrimalityVerificator
    {
        //вероятностный тест на простоту Миллера-Рабина
        public override bool IsPrimal(BigInteger number, int k)
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

            for (int i = 0; i < k; i++)
            {
                BigInteger a = numberGenerator.GenerateNumber(2, number - 2);

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
