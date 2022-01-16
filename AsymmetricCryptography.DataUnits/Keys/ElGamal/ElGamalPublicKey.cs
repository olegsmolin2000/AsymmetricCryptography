namespace AsymmetricCryptography.DataUnits.Keys.ElGamal
{
    /// <summary>
    /// ElGamal private key data unit (P,G,Y)
    /// </summary>
    public sealed class ElGamalPublicKey : AsymmetricKey
    {
        /// <summary>
        /// Random prime number P
        /// </summary>
        public BigInteger P { get; init; }

        /// <summary>
        /// Primitiv root of P
        /// </summary>
        public BigInteger G { get; init; }

        /// <summary>
        /// Key value Y, Y = G^X mod P
        /// </summary>
        public BigInteger Y { get; init; }

        /// <summary>
        /// Initializes new instance of ElGamal public key with a specified values
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="p">Random prime number P value</param>
        /// <param name="g">Primitive root of P value</param>
        /// <param name="y">Key value Y</param>
        public ElGamalPublicKey(int binarySize, BigInteger p, BigInteger g, BigInteger y)
            : base(binarySize, AlgorithmName.ElGamal, KeyType.Public)
        {
            P = p;
            G = g;
            Y = y;
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitElGamalPublicKey(this);
        }
    }
}
