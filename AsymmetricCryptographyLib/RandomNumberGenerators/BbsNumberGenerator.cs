using AsymmetricCryptography;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using System;
using System.Numerics;
    
namespace AsymmetricCryptographyLib.RandomNumberGenerators
{
    public sealed class BbsNumberGenerator : NumberGenerator
    {
        public BbsNumberGenerator(PrimalityVerificator primalityVerificator) 
            : base(primalityVerificator) 
        {
            parametersPrimalityVerificator = new MillerRabinPrimalityVerificator();
            parametersNumberGenerator = new FibonacciNumberGenerator(parametersPrimalityVerificator);

            do
            {
                p = parametersNumberGenerator.GeneratePrimeNumber(1024);
            } while ((p - 3) % 4 != 0);

            do
            {
                q = parametersNumberGenerator.GeneratePrimeNumber(1024);
            } while ((q - 3) % 4 != 0 && q == p);

            m = p * q;
        }

        private PrimalityVerificator parametersPrimalityVerificator;
        private NumberGenerator parametersNumberGenerator;

        BigInteger p;
        BigInteger q;

        BigInteger m;

        public override BigInteger GenerateNumber(int binarySize)
        {
            BigInteger x = parametersNumberGenerator.GenerateNumber(1, BigInteger.Pow(2, binarySize) - 1);

            BigInteger result = 1;

            for (int i = 1; i < binarySize; i++)
            {
                x = (x * x) % m;

                result <<= 1;

                result |= x & 1;
            }

            return result;
        }

        public override BigInteger GenerateNumber(BigInteger min, BigInteger max)
        {
            if (min > max)
                throw new ArgumentException("Min > max");

            int minBitLength = BinaryConverter.GetBinaryLength(min);
            int maxBitLength = BinaryConverter.GetBinaryLength(max);

            BigInteger number;

            do
            {
                int newNumberBitLength = rand.Next(minBitLength, maxBitLength + 1);

                number = GenerateNumber(newNumberBitLength);
            } while (number < min || number > max);

            return number;
        }

        public override BigInteger GeneratePrimeNumber(int binarySize)
        {
            BigInteger number = 0;

            while (!primalityVerificator.IsPrimal(number, 100))
                number = GenerateNumber(binarySize);

            return number;
        }

        public override BigInteger GeneratePrimeNumber(BigInteger min, BigInteger max)
        {
            BigInteger number = 0;

            while (!primalityVerificator.IsPrimal(number, 100))
                number = GenerateNumber(min, max);

            return number;
        }

        public override string ToString()
        {
            return "Blum Blum Shub";
        }
    }
}
