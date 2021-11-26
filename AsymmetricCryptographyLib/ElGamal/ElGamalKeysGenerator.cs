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
            //генерация случайного простого числа p
            BigInteger p = numberGenerator.GeneratePrimeNumber(binarySize);

            //вычисление g - первообразного корня p
            BigInteger g = ModularArithmetic.GetPrimitiveRoot(p);

            //выбирается простое число x, 1 < x < p - 1
            BigInteger x = numberGenerator.GenerateNumber(2, p - 2);

            //вычисляется y=g^x mod p
            BigInteger y = BigInteger.ModPow(g, x, p);

            privateKey = new ElGamalPrivateKey(name, binarySize, x, p, g);
            publicKey = new ElGamalPublicKey(name, binarySize, y, p, g);
        }
    }
}
