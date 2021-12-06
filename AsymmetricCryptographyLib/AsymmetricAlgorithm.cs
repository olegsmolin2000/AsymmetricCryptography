using System.Numerics;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public abstract class AsymmetricAlgorithm
    {
        internal static NumberGenerator NumberGenerator { get; set; }
        internal static PrimalityVerificator PrimalityVerificator { get; set; }
        public static CryptographicHashAlgorithm HashAlgorithm { get; set; }

        protected AsymmetricKey privateKey;
        protected AsymmetricKey publicKey;

        static AsymmetricAlgorithm()
        {
            PrimalityVerificator = new MillerRabinPrimalityVerificator();
            NumberGenerator = new FibonacciNumberGenerator(PrimalityVerificator);
            HashAlgorithm = new SHA_256();
        }

        // выбор сессионного ключа k. случайное целое число, взаимно простое с (p - 1), 1 < k < p - 1
        protected BigInteger GenerateSessionKey(BigInteger p)
        {
            BigInteger sessionKey;

            do
            {
                sessionKey = NumberGenerator.GenerateNumber(2, p - 2);
            } while (!PrimalityVerificator.IsCoprime(sessionKey, p - 1));

            return sessionKey;
        }
    }
}
