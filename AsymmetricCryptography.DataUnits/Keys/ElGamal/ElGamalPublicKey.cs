namespace AsymmetricCryptography.DataUnits.Keys.ElGamal
{
    public sealed class ElGamalPublicKey : AsymmetricKey
    {
        public BigInteger P { get; init; }
        public BigInteger G { get; init; }

        public BigInteger Y { get; init; }

        public ElGamalPublicKey(int binarySize, BigInteger p, BigInteger g, BigInteger y)
            : base(binarySize, AlgorithmName.ElGamal, KeyType.Public)
        {
            P = p;
            G = g;
            Y = y;
        }
    }
}
