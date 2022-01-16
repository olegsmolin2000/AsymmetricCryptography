namespace AsymmetricCryptography.DataUnits.Keys.RSA
{
    /// <summary>
    /// Rsa private key data unit (pair (d,n))
    /// </summary>
    public sealed class RsaPrivateKey : AsymmetricKey
    {
        /// <summary>
        /// Value of private exponent d, d * e == 1 mod (fi(n))
        /// </summary>
        public BigInteger PrivateExponent { get; init; }

        /// <summary>
        /// Value of modulus n, n == p * q
        /// </summary>
        public BigInteger Modulus { get; init; }

        /// <summary>
        /// Initializes new instance of RSA private key with a specified values
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="privateExponent">Private exponent value (d)</param>
        /// <param name="modulus">Modulus value (n)</param>
        public RsaPrivateKey(int binarySize, BigInteger privateExponent, BigInteger modulus)
            : base(binarySize, AlgorithmName.RSA, KeyType.Private)
        {
            PrivateExponent = privateExponent;
            Modulus = modulus;
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitRsaPrivateKey(this);
        }
    }
}
