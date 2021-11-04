using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using System.Numerics;

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
            ElGamalKeyParameter parameters = ElGamalParametersGeneration(name, binarySize);

            ElGamalKeysGeneration(name, parameters, out privateKey, out publicKey);
        }

        public ElGamalKeyParameter ElGamalParametersGeneration(string name, int binarySize)
        {
            //генерация случайного простого числа p
            BigInteger p = numberGenerator.GeneratePrimeNumber(binarySize);

            //вычисление g - первообразного корня p
            BigInteger g = ModularArithmetic.GetPrimitiveRoot(p);

            return new ElGamalKeyParameter(name,binarySize,generationParameters, p, g);
        }

        public void ElGamalKeysGeneration(string name, int binarySize, out ElGamalKeyParameter parameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            parameters = ElGamalParametersGeneration(name, binarySize);

            ElGamalKeysGeneration(name, parameters, out privateKey, out publicKey);
        }

        public void ElGamalKeysGeneration(string name, ElGamalKeyParameter parameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            BigInteger p = parameters.P;
            BigInteger g = parameters.G;

            //выбирается простое число x, 1 < x < p - 1
            BigInteger x = numberGenerator.GenerateNumber(2, p - 2);

            //вычисляется y=g^x mod p
            BigInteger y = BigInteger.ModPow(g, x, p);

            privateKey = new ElGamalPrivateKey(name, parameters.BinarySize, generationParameters, x);
            publicKey = new ElGamalPublicKey(name, parameters.BinarySize, generationParameters , y);
        }
    }
}
