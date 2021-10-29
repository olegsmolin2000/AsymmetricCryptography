using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.ElGamal
{
    public class ElGamalKeysGenerator : KeysGenerator
    {
        public ElGamalKeysGenerator(Parameters parameters)
            : base(parameters)
        {
        }

        public override void GenerateKeyPair(string name, int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            ElGamalKeyParameters parameters = ElGamalParametersGeneration(binarySize);

            ElGamalKeysGeneration(parameters, out privateKey, out publicKey);
        }

        public ElGamalKeyParameters ElGamalParametersGeneration(int binarySize)
        {
            //генерация случайного простого числа p
            BigInteger p = numberGenerator.GeneratePrimeNumber(binarySize);

            //вычисление g - первообразного корня p
            BigInteger g = ModularArithmetic.GetPrimitiveRoot(p);

            return new ElGamalKeyParameters(p, g);
        }

        public void ElGamalKeysGeneration(int binarySize, out ElGamalKeyParameters parameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            parameters = ElGamalParametersGeneration(binarySize);

            ElGamalKeysGeneration(parameters, out privateKey, out publicKey);
        }

        public void ElGamalKeysGeneration(ElGamalKeyParameters parameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            BigInteger p = parameters.P;
            BigInteger g = parameters.G;

            //выбирается простое число x, 1 < x < p - 1
            BigInteger x = numberGenerator.GenerateNumber(2, p - 2);

            //вычисляется y=g^x mod p
            BigInteger y = BigInteger.ModPow(g, x, p);

            privateKey = new ElGamalPrivateKey(parameters, x);
            publicKey = new ElGamalPublicKey(parameters, y);
        }
    }
}
