using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    static class PrimalityVerifications
    {
        //вероятностный тест на простоту Миллера-Рабина
        public static bool IsPrimal(BigInteger number, int k)
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
                BigInteger a = FibonacciNumberGenerator.GenerateNumber(2, number - 2);

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

        //проверка чисел на взаимную простоту
        public static bool IsCoprime(BigInteger num1, BigInteger num2)
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
