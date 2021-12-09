namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    public sealed class DsaPrivateKey : AsymmetricKey
    {
        public BigInteger X { get; init; }

        // TODO: choose private set or init. depends on ef core
        public DsaDomainParameter DomainParameter { get; private set; }

        private DsaPrivateKey(int binarySize) 
            : base(AlgorithmName.DSA, KeyType.Private, binarySize) { }

        public DsaPrivateKey(int binarySize, BigInteger x, DsaDomainParameter domainParameter)
            :this(binarySize)
        {
            X = x;
            DomainParameter = domainParameter;
        }
    }
}
