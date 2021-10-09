using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.DigitalSignatureAlgorithm;

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
            d = ModularArithmetic.GetMultiplicativeModuloReverse(e, euler);

            privateKey = new RsaPrivateKey(n, e, d, p, q);
            publicKey = new RsaPublicKey(e, n);
        }

        //генерация доменных параметров DSA, которые можно использовать для нескольких пользователей
        //L - битовый размер параметра p
        //N - число бит размером, совпадающим с числом бит в значении криптографической хеш функции
        public static DsaDomainParameters DsaDomainParametersGeneration(int L,int N)
        {
            //q - простое число, размер которого в битах совпадает с размерностью в битах значения хеш-функции
            BigInteger q = NumberGenerator.GeneratePrimeNumber(N);

            //поиск простого числа p такого, что (p-1) % q == 0
            BigInteger p;

            //генерируется случайное НЕПРОСТОЕ число длины L-N,
            //умножается на q, прибавляется 1 и результат проверяется на простоту
            do
            {
                p = NumberGenerator.GenerateNumber(L- N);
                p *= q;
                p++;
            } while (!PrimalityVerifications.IsPrimal(p, 1000));

            //вычисляется g по формуле g = h^((p - 1) / q) mod p, такое что g != 1
            //обычно h = 2 подходит
            BigInteger g = BigInteger.ModPow(2, (p - 1) / q, p);

            //если h = 2 не подошло, то оно выбирается из промежутка (1, p - 1)
            while (g <= 1)
            {
                BigInteger h = NumberGenerator.GenerateNumber(1, p - 1);

                g = BigInteger.ModPow(h, (p - 1) / q, p);
            }

            return new DsaDomainParameters(q, p, g);
        }

        public static void DsaKeysGeneration(int L, int N, out DsaPrivateKey privateKey, out DsaPublicKey publicKey)
        {
            DsaDomainParameters parameters = DsaDomainParametersGeneration(L, N);

            DsaKeysGeneration(parameters, out privateKey, out publicKey);
        }

        public static void DsaKeysGeneration(int L, int N, out DsaDomainParameters parameters, out DsaPrivateKey privateKey, out DsaPublicKey publicKey)
        {
            parameters = DsaDomainParametersGeneration(L, N);

            DsaKeysGeneration(parameters, out privateKey, out publicKey);
        }

        public static void DsaKeysGeneration(DsaDomainParameters parameters,out DsaPrivateKey privateKey, out DsaPublicKey publicKey)
        {
            //x - закрытый ключ. случайное число в промежутке (2, q)
            BigInteger x = NumberGenerator.GenerateNumber(2, parameters.Q - 1);

            //y - открытый ключ. y=g^x mod p
            BigInteger y = BigInteger.ModPow(parameters.G, x, parameters.P);

            privateKey = new DsaPrivateKey(parameters, x, y);
            publicKey = new DsaPublicKey(parameters, y);
        }


    }
}
