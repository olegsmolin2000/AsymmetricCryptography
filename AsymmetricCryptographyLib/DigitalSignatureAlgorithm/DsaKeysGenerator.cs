using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using System.Numerics;


namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    public class DsaKeysGenerator : KeysGenerator
    {
        public DsaKeysGenerator(GeneratingParameters parameters)
           : base(parameters) { }

        public override void GenerateKeyPair(string name, int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            DsaDomainParameter domainParameters = DsaDomainParametersGeneration(name, binarySize, hashAlgorithm.GetDigestBitLength());

            DsaKeysGeneration(name, domainParameters, out privateKey, out publicKey);
        }

        public void DsaKeysGeneration(string name, int L, int N, out DsaDomainParameter domainParameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            domainParameters = DsaDomainParametersGeneration(name, L, N);

            DsaKeysGeneration(name, domainParameters, out privateKey, out publicKey);
        }

        // генерация ключей по доменным параметрам
        public void DsaKeysGeneration(string name, DsaDomainParameter domainParameters, out AsymmetricKey privateKey, out AsymmetricKey publicKey)
        {
            //x - закрытый ключ. случайное число в промежутке (2, q)
            BigInteger x = numberGenerator.GenerateNumber(2, domainParameters.Q - 1);

            //y - открытый ключ. y=g^x mod p
            BigInteger y = BigInteger.ModPow(domainParameters.G, x, domainParameters.P);

            privateKey = new DsaPrivateKey(name,domainParameters.BinarySize,domainParameters,x);
            publicKey = new DsaPublicKey(name,domainParameters.BinarySize, domainParameters,y);
        }

        //генерация доменных параметров DSA, которые можно использовать для нескольких пользователей
        //L - битовый размер параметра p
        //N - число бит размером, совпадающим с числом бит в значении криптографической хеш функции
        public DsaDomainParameter DsaDomainParametersGeneration(string name, int L, int N)
        {
            //q - простое число, размер которого в битах совпадает с размерностью в битах значения хеш-функции
            BigInteger q = numberGenerator.GeneratePrimeNumber(N);

            //поиск простого числа p такого, что (p-1) % q == 0
            BigInteger p;

            //генерируется случайное НЕПРОСТОЕ число длины L-N,
            //умножается на q, прибавляется 1 и результат проверяется на простоту
            do
            {
                p = numberGenerator.GenerateNumber(L - N);
                p *= q;
                p++;
            } while (!primalityVerificator.IsPrimal(p, 100));

            //вычисляется g по формуле g = h^((p - 1) / q) mod p, такое что g != 1
            //обычно h = 2 подходит
            BigInteger g = BigInteger.ModPow(2, (p - 1) / q, p);

            //если h = 2 не подошло, то оно выбирается из промежутка (1, p - 1)
            while (g <= 1)
            {
                BigInteger h = numberGenerator.GenerateNumber(1, p - 1);

                g = BigInteger.ModPow(h, (p - 1) / q, p);
            }

            return new DsaDomainParameter(name,L, q, p, g);
        }
    }
}
