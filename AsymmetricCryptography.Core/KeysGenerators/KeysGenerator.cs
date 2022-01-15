using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits.Keys;

namespace AsymmetricCryptography.Core.KeysGenerators
{
    /// <summary>
    /// Represents the base class from which all implementations of keys generators must derived
    /// </summary>
    public abstract class KeysGenerator
    {
        /// <summary>
        /// Number generator used in keys generating
        /// </summary>
        protected NumberGenerator NumberGenerator { get; private init; }

        /// <summary>
        /// Primality verificator used in keys generating
        /// </summary>
        protected PrimalityVerificator PrimalityVerificator { get; private init; }

        /// <summary>
        /// Hash algorithm used in some keys generating
        /// </summary>
        protected HashAlgorithm HashAlgorithm { get; private init; }

        protected KeysGenerator(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm)
        {
            NumberGenerator = numberGenerator;
            PrimalityVerificator = primalityVerificator;
            HashAlgorithm = hashAlgorithm;
        }

        public abstract void GenerateKeys(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey);

        protected void FillGeneratingParameters(AsymmetricKey key)
        {
            // TODO: заполнить ключ параметрами генерации
        }
    }
}
