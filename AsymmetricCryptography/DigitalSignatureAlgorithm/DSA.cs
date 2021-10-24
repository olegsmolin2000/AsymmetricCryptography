using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.ElGamal;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    sealed class DSA:AsymmetricAlgorithm, IDigitalSignatutator
    {
        public override string AlgorithmName => "DSA";

        private new DsaPrivateKey PrivateKey
        {
            get
            {
                return base.PrivateKey as DsaPrivateKey;
            }
            set
            {
                if (value is DsaPrivateKey)
                    base.PrivateKey = value;
            }
        }
        private new DsaPublicKey PublicKey
        {
            get
            {
                return base.PublicKey as DsaPublicKey;
            }
            set
            {
                if (value is DsaPublicKey)
                    base.PublicKey = value;
            }
        }

        public DSA(AsymmetricKey privateKey, AsymmetricKey publicKey, Parameters parameters)
             : base(parameters)
        {
            SetKeys(privateKey, publicKey);
        }

        public DigitalSignature CreateSignature(byte[] data)
        {
            BigInteger q = PrivateKey.Parameters.Q;
            BigInteger p = PrivateKey.Parameters.P;
            BigInteger g = PrivateKey.Parameters.G;

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

            BigInteger q = PublicKey.Parameters.Q;
            BigInteger p = PublicKey.Parameters.P;
            BigInteger g = PublicKey.Parameters.G;

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
