namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    public sealed class DsaPublicKey : AsymmetricKey
    {
        public BigInteger Y { get; init; }

        // TODO: choose private set or init. depends on ef core
        public DsaDomainParameter DomainParameter { get; private set; }


        public DsaPublicKey(int binarySize,BigInteger y)
            : base(binarySize, AlgorithmName.DSA, KeyType.Public)
        {
            this.Y = y;
        }

        public DsaPublicKey(int binarySize, BigInteger y, DsaDomainParameter domainParameter)
            : base(binarySize, AlgorithmName.DSA, KeyType.Public)
        {
            Y = y;
            DomainParameter = domainParameter;
        }
    }
}
