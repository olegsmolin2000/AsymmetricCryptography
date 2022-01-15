using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;

namespace AsymmetricCryptography.Core
{
    public abstract class AsymmetricAlgorithm
    {
        public NumberGenerator NumberGenerator { get; set; }
        public PrimalityVerificator PrimalityVerificator { get; set; }
        public HashAlgorithm HashAlgorithm { get; set; }

        protected AsymmetricAlgorithm(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm)
        {
            NumberGenerator = numberGenerator;
            PrimalityVerificator = primalityVerificator;
            HashAlgorithm = hashAlgorithm;
        }

        /// <summary>
        /// Generating session key for ElGamal scheme
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Coprime with (p - 1)</returns>
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
