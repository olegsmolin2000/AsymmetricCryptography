namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    public sealed class DsaPrivateKey : AsymmetricKey
    {
        public BigInteger X { get; init; }

        // TODO: choose private set or init. depends on ef core
        public DsaDomainParameter DomainParameter { get; private set; }

        public DsaPrivateKey(int binarySize,BigInteger x)
            : base(binarySize, AlgorithmName.DSA, KeyType.Private)
        {
            this.X = x;
        }

        public DsaPrivateKey(int binarySize, BigInteger x, DsaDomainParameter domainParameter)
            : base(binarySize, AlgorithmName.DSA, KeyType.Private)
        {
            X = x;
            DomainParameter = domainParameter;
        }
    }
}
