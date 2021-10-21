using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.RSA
{
    class RsaKeysGenerator : KeysGenerator
    {
        public override void GenerateKeyPair(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            BigInteger p, q;
            BigInteger n, fi;
            BigInteger e, d;

            //генерация простых чисел p и q по заданному количеству бит
            q = p = NumberGenerator.GeneratePrimeNumber(binarySize);
            while (q == p)
                q = NumberGenerator.GeneratePrimeNumber(binarySize);

            //вычисление модуля
            n = p * q;

            //нахождение функции Эйлера от числа n
            fi = (p - 1) * (q - 1);

            //генерация открытой экспоненты e (1 < e < euler), взаимно простой с euler
            do
            {
                e = NumberGenerator.GeneratePrimeNumber(1, fi);
            } while (!PrimalityVerifications.IsCoprime(e, fi));

            //вычисление закрытой экспоненты, d*e (mod euler) =1 ( мультипликативно обратное к числу e по модулю euler)
            d = ModularArithmetic.GetMultiplicativeModuloReverse(e, fi);

            privateKey = new RsaPrivateKey(n, e, d, p, q);
            publicKey = new RsaPublicKey(e, n);
        }
    }
}
