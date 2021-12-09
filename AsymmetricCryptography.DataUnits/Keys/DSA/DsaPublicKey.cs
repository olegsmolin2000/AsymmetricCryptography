namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    public sealed class DsaPublicKey : AsymmetricKey
    {
        public BigInteger Y { get; init; }

        // TODO: choose private set or init. depends on ef core
        public DsaDomainParameter DomainParameter { get; private set; }

        public DsaPublicKey(int binarySize) 
            : base(AlgorithmName.DSA, KeyType.Public, binarySize) { }

        public DsaPublicKey(int binarySize, BigInteger y, DsaDomainParameter domainParameter)
            : this(binarySize)
        {
            Y = y;
            DomainParameter = domainParameter;
        }
    }
}
