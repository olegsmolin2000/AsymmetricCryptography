using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public abstract class KeysGenerator
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;
        protected CryptographicHashAlgorithm hashAlgorithm;

        private string[] generationParameters;

        protected KeysGenerator(GeneratingParameters parameters)
        {
            this.numberGenerator = parameters.numberGenerator;
            this.primalityVerificator = parameters.primalityVerificator;
            this.hashAlgorithm = parameters.hashAlgorithm;

            generationParameters = parameters.GetParametersInfo();

        }

        public abstract void GenerateKeyPair(string name, int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey);
        
        protected void FillGenerationParameters(AsymmetricKey key)
        {
            key.NumberGenerator = generationParameters[0];
            key.PrimalityVerificator = generationParameters[1];
            key.HashAlgorithm = generationParameters[2];
        }
    }
}
