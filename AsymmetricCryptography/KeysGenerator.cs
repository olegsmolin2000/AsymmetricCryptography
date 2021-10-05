using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AsymmetricCryptography.RSA;

namespace AsymmetricCryptography
{
    static class KeysGenerator
    {
        public static void RsaKeysGeneration(int keyBinaryLength,out RsaPrivateKey privateKey,out RsaPublicKey publicKey)
        {
            BigInteger p, q;
            BigInteger n, euler;
            BigInteger e, d;

            //генерация простых чисел p и q по заданному количеству бит
            q = p = NumberGenerator.GeneratePrimeNumber(keyBinaryLength);
            while(q==p)
                q = NumberGenerator.GeneratePrimeNumber(keyBinaryLength);

            //вычисление модуля
            n = p * q;

            //нахождение функции Эйлера от числа n
            euler = (p - 1) * (q - 1);

            //генерация открытой экспоненты e (1 < e < euler), взаимно простой с euler
            do
            {
                e = NumberGenerator.GeneratePrimeNumber(1, euler);
            } while (!PrimalityVerifications.IsCoprime(e,euler));

            //вычисление закрытой экспоненты, d*e (mod euler) =1 ( мультипликативно обратное к числу e по модулю euler)
            d = e.GetMultiplicativeModuloReverse(euler);

            privateKey = new RsaPrivateKey(n, e, d, p, q);
            publicKey = new RsaPublicKey(e, n);
        }
    }
}
