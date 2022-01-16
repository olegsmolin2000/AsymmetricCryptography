namespace AsymmetricCryptography.DataUnits.Keys.RSA
{
    /// <summary>
    /// Rsa public key data unit (pair (e,n))
    /// </summary>
    public sealed class RsaPublicKey : AsymmetricKey
    {
        /// <summary>
        /// Value of public exponent e, 1 < e < fi(n), e is coprime fi(n)
        /// </summary>
        public BigInteger PublicExponent { get; init; }

        /// <summary>
        /// Value of modulus n, n == p * q
        /// </summary>
        public BigInteger Modulus { get; init; }

        /// <summary>
        /// Initializes new instance of RSA private key with a specified values
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="publicExponent">Public exponent value (e)</param>
        /// <param name="modulus">Modulus value (n)</param>
        public RsaPublicKey(int binarySize, BigInteger publicExponent, BigInteger modulus)
            : base(binarySize, AlgorithmName.RSA, KeyType.Public)
        {
            PublicExponent = publicExponent;
            Modulus = modulus;
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitRsaPublicKey(this);
        }
    }
}
