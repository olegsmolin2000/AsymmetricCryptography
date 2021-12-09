namespace AsymmetricCryptography.DataUnits.Keys.ElGamal
{
    public sealed class ElGamalPublicKey : AsymmetricKey
    {
        public BigInteger P { get; init; }
        public BigInteger G { get; init; }

        public BigInteger Y { get; init; }

        private ElGamalPublicKey(int binarySize) 
            : base(AlgorithmName.ElGamal, KeyType.Public, binarySize) { }

        public ElGamalPublicKey(int binarySize, BigInteger p, BigInteger g, BigInteger y)
            : this(binarySize)
        {
            P = p;
            G = g;
            Y = y;
        }
    }
}
