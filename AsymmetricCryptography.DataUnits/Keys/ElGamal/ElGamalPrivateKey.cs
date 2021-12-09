namespace AsymmetricCryptography.DataUnits.Keys.ElGamal
{
    public sealed class ElGamalPrivateKey : AsymmetricKey
    {
        public BigInteger P { get; init; }
        public BigInteger G { get; init; }

        public BigInteger X { get; init; }

        private ElGamalPrivateKey(int binarySize) 
            : base(AlgorithmName.ElGamal, KeyType.Private, binarySize) { }

        public ElGamalPrivateKey(int binarySize, BigInteger p, BigInteger g, BigInteger x)
            :this(binarySize)
        {
            P = p;
            G = g;
            X = x;
        }
    }
}
