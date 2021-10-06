using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    static class ModularArithmetic
    {
        //функция получения числа по модулю
        //нужна для того, чтобы из отрицательных чисел правильно получать модуль
        public static BigInteger Modulus(BigInteger value,BigInteger modulus)
        {
            value = value % modulus;

            if (value < 0)
                return value + modulus;
            else
                return value;
        }

        //функция получения примитивного корня
        //код спизжен, поэтому сказать ничего не могу
        //только то, что работает быстрее наивного метода
        public static BigInteger GetPrimitiveRoot(BigInteger number)
        {
            List<BigInteger> fact = new List<BigInteger>();

            BigInteger phi = number - 1, n = phi;

            for (int i = 2; i * i <= n; ++i)
                if (n % i == 0)
                {
                    fact.Add(i);
                    while (n % i == 0)
                        n /= i;
                }

            if (n > 1)
                fact.Add(n);

            for (BigInteger res = 2; res <= number; ++res)
            {
                bool ok = true;
                for (int i = 0; i < fact.Count && ok; ++i)
                    ok &= BigInteger.ModPow(res, phi / fact[i], number) != 1;
                if (ok) return res;
            }
            return -1;
        }

        //функция получения обратного числа по модулю
        //реализована с помощью расширенного алгоритма Евклида
        public static BigInteger GetMultiplicativeModuloReverse(BigInteger number, BigInteger mod)
        {
            BigInteger x, y;

            BigInteger result = GcdExtended(number, mod, out x, out y);

            return (x % mod + mod) % mod;
        }

        //расширенный алгоритм Евклида
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
