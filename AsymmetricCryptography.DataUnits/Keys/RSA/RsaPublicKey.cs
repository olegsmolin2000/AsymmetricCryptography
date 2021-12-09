namespace AsymmetricCryptography.DataUnits.Keys.RSA
{
    public sealed class RsaPublicKey : AsymmetricKey
    {
        public BigInteger PublicExponent { get; init; }
        public BigInteger Modulus { get; init; }

        public RsaPublicKey(int binarySize, BigInteger publicExponent, BigInteger modulus)
            : base(binarySize, AlgorithmName.RSA, KeyType.Public)
        {
            PublicExponent = publicExponent;
            Modulus = modulus;
        }
    }
}
