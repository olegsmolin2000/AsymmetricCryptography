using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;

namespace AsymmetricCryptography.Core.KeysGenerators
{
    public sealed class ElGamalKeysGenerator : KeysGenerator
    {
        public ElGamalKeysGenerator(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityVerificator, hashAlgorithm) { }

        public override void GenerateKeys(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            //генерация случайного простого числа p
            BigInteger p = NumberGenerator.GeneratePrimeNumber(binarySize);

            //вычисление g - первообразного корня p
            BigInteger g = ModularArithmetic.GetPrimitiveRoot(p);

            //выбирается простое число x, 1 < x < p - 1
            BigInteger x = NumberGenerator.GenerateNumber(2, p - 2);

            //вычисляется y=g^x mod p
            BigInteger y = BigInteger.ModPow(g, x, p);

            privateKey = new ElGamalPrivateKey(binarySize, p, g, x);
            publicKey = new ElGamalPublicKey(binarySize, p, g, y);

            FillGeneratingParameters(privateKey);
            FillGeneratingParameters(publicKey);
        }
    }
}
