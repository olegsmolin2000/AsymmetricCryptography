using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using System.Numerics;

namespace AsymmetricCryptography.RSA
{
    public class RsaKeysGenerator : KeysGenerator
    {
        public RsaKeysGenerator(GeneratingParameters parameters) 
            : base(parameters) { }

        public bool IsFixedPublicExponent { get; set; } = false;

        public override void GenerateKeyPair(string name, int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            BigInteger p, q;
            BigInteger n, fi;
            BigInteger e, d;

            //генерация простых чисел p и q по заданному количеству бит
            q = p = numberGenerator.GeneratePrimeNumber(binarySize);
            while (q == p)
                q = numberGenerator.GeneratePrimeNumber(binarySize);

            //вычисление модуля
            n = p * q;

            //нахождение функции Эйлера от числа n
            fi = (p - 1) * (q - 1);

            if (IsFixedPublicExponent)
            {
                e = 65537;
            }
            else
            {
                //генерация открытой экспоненты e (1 < e < euler), взаимно простой с euler
                do
                {
                    e = numberGenerator.GeneratePrimeNumber(1, fi);
                } while (!primalityVerificator.IsCoprime(e, fi));
            }
            

            //вычисление закрытой экспоненты, d*e (mod euler) = 1 ( мультипликативно обратное к числу e по модулю euler)
            d = ModularArithmetic.GetMultiplicativeModuloReverse(e, fi);

            privateKey = new RsaPrivateKey(name, binarySize, n, d);
            publicKey = new RsaPublicKey(name, binarySize, n, e);
        }
    }
}
