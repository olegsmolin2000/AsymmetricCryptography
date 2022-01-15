using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.RSA;

namespace AsymmetricCryptography.Core.KeysGenerators
{
    public sealed class RsaKeysGenerator : KeysGenerator
    {
        public bool FixedPublicExponent { get; set; } = false;

        public RsaKeysGenerator(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityVerificator, hashAlgorithm) { }

        public override void GenerateKeys(int binarySize, out AsymmetricKey privateKey, AsymmetricKey publicKey)
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

            if (FixedPublicExponent)
                e = 65537;
            else
            {
                //генерация открытой экспоненты e (1 < e < euler), взаимно простой с euler
                do
                {
                    e = NumberGenerator.GeneratePrimeNumber(1, fi);
                } while (!PrimalityVerificator.IsCoprime(e, fi));
            }

            //вычисление закрытой экспоненты, d*e (mod euler) = 1 ( мультипликативно обратное к числу e по модулю euler)
            d = ModularArithmetic.GetModularMultiplicativeInverse(e, fi);

            privateKey = new RsaPrivateKey(binarySize, n, d);

            publicKey = new RsaPublicKey(binarySize, n, e);

            FillGeneratingParameters(privateKey);
            FillGeneratingParameters(publicKey);
        }
    }
}
