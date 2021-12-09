namespace AsymmetricCryptography.DataUnits.Keys.ElGamal
{
    public sealed class ElGamalPrivateKey : AsymmetricKey
    {
        public BigInteger P { get; init; }
        public BigInteger G { get; init; }

        public BigInteger X { get; init; }

        public ElGamalPrivateKey(int binarySize, BigInteger p, BigInteger g, BigInteger x)
            : base(binarySize, AlgorithmName.ElGamal, KeyType.Private)
        {
            P = p;
            G = g;
            X = x;
        }
    }
}
