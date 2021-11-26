using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptographyDAL.Entities.Keys;

namespace AsymmetricCryptography
{
    public abstract class KeysGenerator
    {
        protected NumberGenerator numberGenerator;
        protected PrimalityVerificator primalityVerificator;
        protected CryptographicHashAlgorithm hashAlgorithm;

        protected string[] generationParameters;

        protected KeysGenerator(GeneratingParameters parameters)
        {
            this.numberGenerator = parameters.numberGenerator;
            this.primalityVerificator = parameters.primalityVerificator;
            this.hashAlgorithm = parameters.hashAlgorithm;

            generationParameters = new string[3];

            this.generationParameters[0] = numberGenerator.ToString();
            this.generationParameters[1] = primalityVerificator.ToString();
            this.generationParameters[2] = hashAlgorithm.ToString();

        }

        public abstract void GenerateKeyPair(string name, int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey); 
    }
}
