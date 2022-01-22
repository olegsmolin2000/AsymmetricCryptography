using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;

namespace AsymmetricCryptography.Core.KeysGenerators
{
    public sealed class DsaKeysGenerator : KeysGenerator
    {
        public int DigestBitLength
        {
            get => HashAlgorithm.DigestBitSize;
        }

        public DsaKeysGenerator(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityVerificator, hashAlgorithm) { }

        public DsaKeysGenerator(RandomNumberGenerator numberGenerator, PrimalityTest primalityTest, CryptographicHashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityTest, hashAlgorithm) {}

        public override void GenerateKeys(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            DsaDomainParameter domainParameters = DsaDomainParametersGeneration(binarySize, HashAlgorithm.DigestBitSize);

            DsaKeysGeneration(domainParameters, out privateKey, out publicKey);
        }

        /// <summary>
        /// Generating instances of DSA keys and domain parameters
        /// </summary>
        /// <param name="L">Binary size of P, must be greater than N</param>
        /// <param name="N">Digest bit length of used hash algorithm</param>
        /// <param name="domainParameters"></param>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public void DsaKeysGeneration(int L, int N, out DsaDomainParameter domainParameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            domainParameters = DsaDomainParametersGeneration(L, N);

            DsaKeysGeneration(domainParameters, out privateKey, out publicKey);
        }

        /// <summary>
        /// DSA keys generating by domain parameters
        /// </summary>
        /// <param name="domainParameters"></param>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public void DsaKeysGeneration(DsaDomainParameter domainParameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            //x - закрытый ключ. случайное число в промежутке (2, q)
            BigInteger x = NumberGenerator.GenerateNumber(2, domainParameters.Q - 1);

            //y - открытый ключ. y=g^x mod p
            BigInteger y = BigInteger.ModPow(domainParameters.G, x, domainParameters.P);

            privateKey = new DsaPrivateKey(domainParameters.BinarySize, x, domainParameters);
            publicKey = new DsaPublicKey(domainParameters.BinarySize, y, domainParameters);

            FillGeneratingParameters(privateKey);
            FillGeneratingParameters(publicKey);
        }

        /// <summary>
        /// Generating only DSA domain parameters
        /// </summary>
        /// <param name="L">Binary size of P, must be greater than N</param>
        /// <param name="N">Digest bit length of used hash algorithm</param>
        /// <returns>Instance of DSA domain parameter</returns>
        public DsaDomainParameter DsaDomainParametersGeneration(int L, int N)
        {
            //q - простое число, размер которого в битах совпадает с размерностью в битах значения хеш-функции
            BigInteger q = NumberGenerator.GeneratePrimeNumber(N);

            //поиск простого числа p такого, что (p-1) % q == 0
            BigInteger p;

            //генерируется случайное НЕПРОСТОЕ число длины L-N,
            //умножается на q, прибавляется 1 и результат проверяется на простоту
            do
            {
                p = NumberGenerator.GenerateNumber(L - N);
                p *= q;
                p++;
            } while (!PrimalityVerificator.IsPrime(p));

            //вычисляется g по формуле g = h^((p - 1) / q) mod p, такое что g != 1
            //обычно h = 2 подходит
            BigInteger g = BigInteger.ModPow(2, (p - 1) / q, p);

            //если h = 2 не подошло, то оно выбирается из промежутка (1, p - 1)
            while (g <= 1)
            {
                BigInteger h = NumberGenerator.GenerateNumber(1, p - 1);

                g = BigInteger.ModPow(h, (p - 1) / q, p);
            }

            DsaDomainParameter domainParameter = new DsaDomainParameter(L, q, p, g);

            FillGeneratingParameters(domainParameter);

            return domainParameter;
        }
    }
}
