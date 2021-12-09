namespace AsymmetricCryptography.DataUnits.Keys.RSA
{
    public sealed class RsaPrivateKey : AsymmetricKey
    {
        public BigInteger PrivateExponent { get; init; }
        public BigInteger Modulus { get; init; }

        private RsaPrivateKey(int binarySize)
            : base(AlgorithmName.RSA, KeyType.Private, binarySize) { }

        public RsaPrivateKey(int binarySize, BigInteger privateExponent, BigInteger modulus)
            : this(binarySize)
        {
            PrivateExponent = privateExponent;
            Modulus = modulus;
        }
    }
}
