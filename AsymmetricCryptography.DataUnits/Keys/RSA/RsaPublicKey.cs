namespace AsymmetricCryptography.DataUnits.Keys.RSA
{
    public sealed class RsaPublicKey : AsymmetricKey
    {
        public BigInteger PublicExponent { get; init; }
        public BigInteger Modulus { get; init; }

        private RsaPublicKey(int binarySize) 
            : base(AlgorithmName.RSA, KeyType.Public, binarySize) { }
        
        public RsaPublicKey(int binarySize, BigInteger publicExponent, BigInteger modulus)
            : this(binarySize)
        {
            PublicExponent = publicExponent;
            Modulus = modulus;
        }
    }
}
