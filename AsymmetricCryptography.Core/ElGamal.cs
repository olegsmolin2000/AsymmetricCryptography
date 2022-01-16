using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits.DigitalSignatures;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;

namespace AsymmetricCryptography.Core
{
    public sealed class ElGamal : AsymmetricAlgorithm,IEncryptor,ISignatutator
    {
        public ElGamal(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityVerificator, hashAlgorithm) { }

        public byte[] Encrypt(byte[] data, AsymmetricKey publicKey)
        {
            var key = publicKey as ElGamalPublicKey;

            if (key == null)
                throw new ArgumentException("Not ElGamal public key");

            //получение параметров
            BigInteger p = key.P;
            BigInteger g = key.G;

            //получение открытого ключа
            BigInteger y = key.Y;

            //вычисление размера блока
            int blockSize = BlockConverter.GetBlockSize(p);

            //перевод массива байтов в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(data, blockSize);

            //в список будут заноситься байты, полученные из зашифрованных блоков
            List<byte> encryptedBytes = new List<byte>();

            //шифрование
            for (int i = 0; i < blocks.Length; i++)
            {
                // выбирается сессионный ключ k. случайное целое число, взаимно простое с (p - 1)
                // 1 < k < p - 1
                BigInteger k = GenerateSessionKey(p);

                // a = g^k mod p
                BigInteger a = BigInteger.ModPow(g, k, p);
                // b = (y^x * M) mod p

                BigInteger b = ModularArithmetic.Modulus(BigInteger.ModPow(y, k, p) * blocks[i], p);

                // пара (a,b) - шифротекст
                encryptedBytes.AddRange(BlockConverter.BlockToBytes(a, blockSize + 1));
                encryptedBytes.AddRange(BlockConverter.BlockToBytes(b, blockSize + 1));
            }

            //в результате получается массив байтов зашифрованных данных
            return encryptedBytes.ToArray();
        }

        public byte[] Decrypt(byte[] encryptedData, AsymmetricKey privateKey)
        {
            var key = privateKey as ElGamalPrivateKey;

            if (key == null)
                throw new ArgumentException("Not ElGamal private key");

            // получение параметров
            BigInteger p = key.P;
            BigInteger g = key.G;

            // получение закрытого ключа
            BigInteger x = key.X;

            //вычисление размера блоков
            int blockSize = BlockConverter.GetBlockSize(p);

            //перевод зашифрованных данных в блоки BigInt
            BigInteger[] blocks = BlockConverter.BytesToBlocks(encryptedData, blockSize + 1);

            //в список будет заноситься результат дешифровки
            List<byte> decryptedBytes = new List<byte>();

            // дешифровка
            for (int i = 0; i < blocks.Length; i += 2)
            {
                BigInteger a = blocks[i];
                BigInteger b = blocks[i + 1];

                // M = (b * (a^x)^-1) mod p
                BigInteger decryption = ModularArithmetic.Modulus(b * ModularArithmetic.GetModularMultiplicativeInverse(BigInteger.ModPow(a, x, p), p), p);

                //в список добавляются расшифрованные байты
                decryptedBytes.AddRange(BlockConverter.BlockToBytes(decryption, blockSize));
            }

            while (decryptedBytes[0] == 0)
                decryptedBytes.RemoveAt(0);

            if (decryptedBytes.Count % 2 != 0)
                decryptedBytes.Insert(0, 0);

            return decryptedBytes.ToArray();
        }

        public DigitalSignature CreateSignature(byte[] data, AsymmetricKey privateKey)
        {
            //вычисление хеша по криптографической хеш функции
            BigInteger hash = new BigInteger(HashAlgorithm.GetHash(data));

            var key = privateKey as ElGamalPrivateKey;

            if (key == null)
                throw new ArgumentException("Not ElGamal private key");

            //получение параметров
            BigInteger p = key.P;
            BigInteger g = key.G;

            //хеш берётся по модулю p - 1, чтобы не было проблем с ним 
            hash = ModularArithmetic.Modulus(hash, p - 1);

            //выбор сессионного ключа
            BigInteger k = GenerateSessionKey(p);

            // r = g^x mod p
            BigInteger r = BigInteger.ModPow(g, k, p);

            //s = ((H - x * r) * k^-1) mod (p - 1)
            BigInteger reverseK = ModularArithmetic.GetModularMultiplicativeInverse(k, p - 1);
            hash -= key.X * r;

            BigInteger s = ModularArithmetic.Modulus(hash * reverseK, p - 1);

            //подписью является пара (r, s)
            return new ElGamalDigitalSignature(r, s);
        }

        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, AsymmetricKey publicKey)
        {
            var digitalSignature = signature as ElGamalDigitalSignature;
            var key = publicKey as ElGamalPublicKey;

            if (key == null)
                throw new ArgumentException("Not ElGamal public key");
            if (digitalSignature == null)
                throw new ArgumentException("Not ElGamal digital signature");

            //получение значений подписи
            BigInteger r = digitalSignature.R;
            BigInteger s = digitalSignature.S;

            //получение параметров
            BigInteger p = key.P;
            BigInteger g = key.G;

            //если условия ниже не выполняются то подпись точно неверна
            if (r <= 0 || r >= p)
                return false;

            if (s <= 0 || s >= p - 1)
                return false;

            //вычисление хеша по криптографической хеш функции
            BigInteger hash = new BigInteger(HashAlgorithm.GetHash(data));

            //хеш берётся по модулю p - 1, чтобы не было проблем с ним 
            hash = ModularArithmetic.Modulus(hash, p - 1);

            BigInteger y = key.Y;

            //вычисление левой части (y^r * r^s) mod p
            BigInteger left = ModularArithmetic.Modulus(BigInteger.ModPow(y, r, p) * BigInteger.ModPow(r, s, p), p);

            //вычисление правой части g^m mod p
            BigInteger right = BigInteger.ModPow(g, hash, p);

            //если левая и правая части равны то подпись верна
            return left == right;
        }
    }
}
