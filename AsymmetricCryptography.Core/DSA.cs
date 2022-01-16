using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits.DigitalSignatures;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;

namespace AsymmetricCryptography.Core
{
    public sealed class DSA : AsymmetricAlgorithm, ISignatutator
    {
        public DSA(NumberGenerator numberGenerator, PrimalityVerificator primalityVerificator, HashAlgorithm hashAlgorithm) 
            : base(numberGenerator, primalityVerificator, hashAlgorithm) { }

        public DigitalSignature CreateSignature(byte[] data, AsymmetricKey privateKey)
        {
            var key = privateKey as DsaPrivateKey;

            if (key == null)
                throw new ArgumentException("Not DSA private key");

            var domainParameter = key.DomainParameter;

            if (domainParameter == null)
                throw new NullReferenceException("Domain parameter null reference");

            BigInteger q = domainParameter.Q;
            BigInteger p = domainParameter.P;
            BigInteger g = domainParameter.G;

            BigInteger r = 0, s = 0;

            while (r == 0 || s == 0)
            {
                //генерация сессионного ключа k, 0 < k < q
                BigInteger k = GenerateSessionKey(q);

                //r = (g^k mod p) mod q
                r = BigInteger.ModPow(g, k, p);

                r = ModularArithmetic.Modulus(r, q);

                BigInteger hash = new BigInteger(HashAlgorithm.GetHash(data));

                //вычисление k^-1 mod q
                BigInteger reverseK = ModularArithmetic.GetModularMultiplicativeInverse(k, q);

                //вычисление (H(m) + x * r) mod q
                hash += key.X * r;

                //s= (k^-1)*(H(m) + x * r) mod q
                s = ModularArithmetic.Modulus(reverseK * hash, q);
            }

            //подписью является пара (r,s)
            return new ElGamalDigitalSignature(r, s);
        }

        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, AsymmetricKey publicKey)
        {
            var digitalSignature = signature as ElGamalDigitalSignature;
            var key = publicKey as DsaPublicKey;
            var domainParameter = key.DomainParameter;

            if (digitalSignature == null)
                throw new ArgumentException("Not ElGamal digital signature");

            if (key == null)
                throw new ArgumentException("Not DSA public key");

            if (domainParameter == null)
                throw new NullReferenceException("Domain parameter null reference");

            BigInteger q = domainParameter.Q;
            BigInteger p = domainParameter.P;
            BigInteger g = domainParameter.G;

            //вычисление w = s^-1 mod q
            BigInteger w = ModularArithmetic.GetModularMultiplicativeInverse(digitalSignature.S, q);

            BigInteger hash = new BigInteger(HashAlgorithm.GetHash(data));

            //u1 = H(m) * w mod q
            BigInteger u1 = ModularArithmetic.Modulus(hash * w, q);
            //u2 = r * w mod q
            BigInteger u2 = ModularArithmetic.Modulus(digitalSignature.R * w, q);

            //v = (g^u1 * y^u2 mod p) mod q
            g = BigInteger.ModPow(g, u1, p);
            BigInteger y = BigInteger.ModPow(key.Y, u2, p);

            BigInteger v = ModularArithmetic.Modulus(ModularArithmetic.Modulus(g * y, p), q);

            //если v == r то подпись верна
            return v == digitalSignature.R;
        }
    }
}
