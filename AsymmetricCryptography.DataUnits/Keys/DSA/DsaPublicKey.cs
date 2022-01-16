namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    /// <summary>
    /// Dsa public key data unit (Y, DomainParameter)
    /// </summary>
    public sealed class DsaPublicKey : AsymmetricKey
    {
        /// <summary>
        /// Key value Y, Y = G^X mod P
        /// </summary>
        public BigInteger Y { get; init; }

        /// <summary>
        /// DomainParameter which generate this key
        /// </summary>
        public DsaDomainParameter DomainParameter { get; set; }

        /// <summary>
        /// Initializes new instance of DSA public key without setting domain parameters
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="y">Key value Y, Y = G^X mod P</param>
        public DsaPublicKey(int binarySize,BigInteger y)
            : base(binarySize, AlgorithmName.DSA, KeyType.Public)
        {
            this.Y = y;
        }

        /// <summary>
        /// Initializes new instance of DSA public key with setting domain parameters
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="y">Key value Y, Y = G^X mod P</param>
        /// <param name="domainParameter">DomainParameter which generate this key</param>
        public DsaPublicKey(int binarySize, BigInteger y, DsaDomainParameter domainParameter)
            : base(binarySize, AlgorithmName.DSA, KeyType.Public)
        {
            Y = y;
            DomainParameter = domainParameter;
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitDsaPublicKey(this);
        }
    }
}