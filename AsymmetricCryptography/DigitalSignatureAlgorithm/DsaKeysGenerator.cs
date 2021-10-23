using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    class DsaKeysGenerator : KeysGenerator
    {
        public DsaKeysGenerator(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator) : base(numberGenerator, primalityVerificator)
        {
        }

        public override void GenerateKeyPair(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            throw new NotImplementedException();
        }

        public void DsaKeysGeneration(int L, int N, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            DsaDomainParameters parameters = DsaDomainParametersGeneration(L, N);

            DsaKeysGeneration(parameters, out privateKey, out publicKey);
        }

        public void DsaKeysGeneration(int L, int N, out DsaDomainParameters parameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            parameters = DsaDomainParametersGeneration(L, N);

            DsaKeysGeneration(parameters, out privateKey, out publicKey);
        }

        // генерация ключей по доменным параметрам
        public void DsaKeysGeneration(DsaDomainParameters parameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            //x - закрытый ключ. случайное число в промежутке (2, q)
            BigInteger x = numberGenerator.GenerateNumber(2, parameters.Q - 1);

            //y - открытый ключ. y=g^x mod p
            BigInteger y = BigInteger.ModPow(parameters.G, x, parameters.P);

            privateKey = new DsaPrivateKey(parameters, x, y);
            publicKey = new DsaPublicKey(parameters, y);
        }

        //генерация доменных параметров DSA, которые можно использовать для нескольких пользователей
        //L - битовый размер параметра p
        //N - число бит размером, совпадающим с числом бит в значении криптографической хеш функции
        public DsaDomainParameters DsaDomainParametersGeneration(int L, int N)
        {
            //q - простое число, размер которого в битах совпадает с размерностью в битах значения хеш-функции
            BigInteger q = numberGenerator.GeneratePrimeNumber(N);

            //поиск простого числа p такого, что (p-1) % q == 0
            BigInteger p;

            //генерируется случайное НЕПРОСТОЕ число длины L-N,
            //умножается на q, прибавляется 1 и результат проверяется на простоту
            do
            {
                p = numberGenerator.GenerateNumber(L - N);
                p *= q;
                p++;
            } while (!primalityVerificator.IsPrimal(p, 1000));

            //вычисляется g по формуле g = h^((p - 1) / q) mod p, такое что g != 1
            //обычно h = 2 подходит
            BigInteger g = BigInteger.ModPow(2, (p - 1) / q, p);

            //если h = 2 не подошло, то оно выбирается из промежутка (1, p - 1)
            while (g <= 1)
            {
                BigInteger h = numberGenerator.GenerateNumber(1, p - 1);

                g = BigInteger.ModPow(h, (p - 1) / q, p);
            }

            return new DsaDomainParameters(q, p, g);
        }
    }
}
