namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    /// <summary>
    /// Dsa private key data unit (X, DomainParameter)
    /// </summary>
    public sealed class DsaPrivateKey : AsymmetricKey
    {
        /// <summary>
        /// Key Value X, X is random number (0,Q)
        /// </summary>
        public BigInteger X { get; init; }

        /// <summary>
        /// DomainParameter which generate this key
        /// </summary>
        public DsaDomainParameter DomainParameter { get; set; }

        /// <summary>
        /// Initializes new instance of DSA private key without setting domain parameters
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="x">KeyValue X, X is random number (0,Q)</param>
        public DsaPrivateKey(int binarySize,BigInteger x)
            : base(binarySize, AlgorithmName.DSA, KeyType.Private)
        {
            this.X = x;
        }

        /// <summary>
        /// Initializes new instance of DSA private key with setting domain parameters
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="x">KeyValue X, X is random number (0,Q)</param>
        /// <param name="domainParameter">DomainParameter which generate this key</param>
        public DsaPrivateKey(int binarySize, BigInteger x, DsaDomainParameter domainParameter)
            : base(binarySize, AlgorithmName.DSA, KeyType.Private)
        {
            X = x;
            DomainParameter = domainParameter;
        }
    }
}
