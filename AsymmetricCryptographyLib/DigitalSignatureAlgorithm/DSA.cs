using System.Numerics;
using AsymmetricCryptography.ElGamal;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    public sealed class DSA:AsymmetricAlgorithm, IDigitalSignatutator
    {
        public DSA(GeneratingParameters parameters) 
            : base(parameters) { }

        public DsaPrivateKey PrivateKey
        {
            get
            {
                return base.privateKey as DsaPrivateKey;
            }

            set
            {
                if (value is DsaPrivateKey)
                    base.privateKey = value;
            }
        }

        public DsaPublicKey PublicKey
        {
            get
            {
                return base.publicKey as DsaPublicKey;
            }

            set
            {
                if (value is DsaPublicKey)
                    base.publicKey = value;
            }
        }

        public DigitalSignature CreateSignature(byte[] data)
        {
            BigInteger q = PrivateKey.DomainParameter.Q;
            BigInteger p = PrivateKey.DomainParameter.P;
            BigInteger g = PrivateKey.DomainParameter.G;

            BigInteger r = 0, s = 0;

            while (r == 0 || s == 0)
            {
                //генерация сессионного ключа k, 0 < k < q
                BigInteger k = GenerateSessionKey(q);

                //r = (g^k mod p) mod q
                r = BigInteger.ModPow(g, k, p);

                r = ModularArithmetic.Modulus(r, q);
               
                BigInteger hash = new BigInteger(hashAlgorithm.GetHash(data));

                //вычисление k^-1 mod q
                BigInteger reverseK = ModularArithmetic.GetMultiplicativeModuloReverse(k, q);

                //вычисление (H(m) + x * r) mod q
                hash += PrivateKey.X * r;

                //s= (k^-1)*(H(m) + x * r) mod q
                s = ModularArithmetic.Modulus(reverseK * hash, q);
            }

            //подписью является пара (r,s)
            return new ElGamalDigitalSignature(r, s);
        }

        public bool VerifyDigitalSignature(DigitalSignature signature ,byte[] data)
        {
            ElGamalDigitalSignature digitalSignature = (ElGamalDigitalSignature)signature;

            BigInteger q = PublicKey.DomainParameter.Q;
            BigInteger p = PublicKey.DomainParameter.P;
            BigInteger g = PublicKey.DomainParameter.G;

            //вычисление w = s^-1 mod q
            BigInteger w = ModularArithmetic.GetMultiplicativeModuloReverse(digitalSignature.S, q);

            BigInteger hash = new BigInteger(hashAlgorithm.GetHash(data));

            //u1 = H(m) * w mod q
            BigInteger u1 = ModularArithmetic.Modulus(hash * w, q);
            //u2 = r * w mod q
            BigInteger u2 = ModularArithmetic.Modulus(digitalSignature.R * w, q);

            //v = (g^u1 * y^u2 mod p) mod q
            g = BigInteger.ModPow(g, u1, p);
            BigInteger y = BigInteger.ModPow(PublicKey.Y, u2, p);

            BigInteger v = ModularArithmetic.Modulus(ModularArithmetic.Modulus(g * y, p), q);

            //если v == r то подпись верна
            return v == digitalSignature.R;
        }
    }
}
