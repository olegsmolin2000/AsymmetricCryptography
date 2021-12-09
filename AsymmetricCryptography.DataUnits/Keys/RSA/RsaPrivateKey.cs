namespace AsymmetricCryptography.DataUnits.Keys.RSA
{
    public sealed class RsaPrivateKey : AsymmetricKey
    {
        public BigInteger PrivateExponent { get; init; }
        public BigInteger Modulus { get; init; }
            
        public RsaPrivateKey(int binarySize, BigInteger privateExponent, BigInteger modulus)
            : base(binarySize, AlgorithmName.RSA, KeyType.Private)
        {
            PrivateExponent = privateExponent;
            Modulus = modulus;
        }
    }
}
