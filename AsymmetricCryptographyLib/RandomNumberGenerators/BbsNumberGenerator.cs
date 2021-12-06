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
                p = parametersNumberGenerator.GeneratePrimeNumber(512);
            } while ((p - 3) % 4 != 0);

            do
            {
                q = parametersNumberGenerator.GeneratePrimeNumber(512);
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
            BigInteger x = parametersNumberGenerator.GenerateNumber(binarySize);

            BigInteger result = 1;

            for (int i = 1; i < binarySize; i++)
            {
                x = (x * x) % m;

                result <<= 1;

                result |= x & 1;
            }

            return result;
        }

        public override string ToString()
        {
            return "Blum Blum Shub";
        }
    }
}
