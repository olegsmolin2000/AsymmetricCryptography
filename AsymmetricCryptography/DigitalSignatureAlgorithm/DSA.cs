using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography.DigitalSignatureAlgorithm
{
    static class DSA
    {
        public static ElGamalDigitalSignature CreateSignature(byte[] message,DsaPrivateKey privateKey,CryptographicHashAlgorithm hashAlgorithm)
        {
            BigInteger q = privateKey.Parameters.Q;
            BigInteger p = privateKey.Parameters.P;
            BigInteger g = privateKey.Parameters.G;

            BigInteger r = 0, s = 0;

            while (r == 0 || s == 0)
            {
                //генерация сессионного ключа k, 0 < k < q
                BigInteger k = GenerateSessionKey(q);

                //r = (g^k mod p) mod q
                r = BigInteger.ModPow(g, k, p);

                r = ModularArithmetic.Modulus(r, q);
               
                BigInteger hash = new BigInteger(hashAlgorithm.GetHash(message));

                hash = ModularArithmetic.Modulus(hash, p);

                //вычисление k^-1 mod q
                BigInteger reverseK = ModularArithmetic.GetMultiplicativeModuloReverse(k, q);

                //вычисление (H(m) + x * r) mod q
                hash += privateKey.X * r;

                //s= (k^-1)*(H(m) + x * r) mod q
                s = ModularArithmetic.Modulus(reverseK * hash, q);
            }

            //подписью является пара (r,s)
            return new ElGamalDigitalSignature(r, s);
        }

        public static bool VerifySignature(byte[] message,ElGamalDigitalSignature signature,DsaPublicKey publicKey,CryptographicHashAlgorithm hashAlgorithm)
        {
            BigInteger q = publicKey.Parameters.Q;
            BigInteger p = publicKey.Parameters.P;
            BigInteger g = publicKey.Parameters.G;

            //вычисление w = s^-1 mod q
            BigInteger w = ModularArithmetic.GetMultiplicativeModuloReverse(signature.S, q);

            BigInteger hash = new BigInteger(hashAlgorithm.GetHash(message));

            hash = ModularArithmetic.Modulus(hash, p);

            //u1 = H(m) * w mod q
            BigInteger u1 = ModularArithmetic.Modulus(hash * w, q);
            //u2 = r * w mod q
            BigInteger u2 = ModularArithmetic.Modulus(signature.R * w, q);

            //v = (g^u1 * y^u2 mod p) mod q
            g = BigInteger.ModPow(g, u1, p);
            BigInteger y = BigInteger.ModPow(publicKey.Y, u2, p);

            BigInteger v = ModularArithmetic.Modulus(ModularArithmetic.Modulus(g * y, p), q);

            //если v == r то подпись верна
            return v == signature.R;
        }

        private static BigInteger GenerateSessionKey(BigInteger q)
        {
            return NumberGenerator.GenerateNumber(2, q - 1);
        }
    }
}
