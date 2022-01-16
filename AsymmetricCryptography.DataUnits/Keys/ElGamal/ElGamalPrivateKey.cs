namespace AsymmetricCryptography.DataUnits.Keys.ElGamal
{
    /// <summary>
    /// ElGamal private key data unit (P,G,X)
    /// </summary>
    public sealed class ElGamalPrivateKey : AsymmetricKey
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
        /// Key value X, X is random number (1, P - 1)
        /// </summary>
        public BigInteger X { get; init; }

        /// <summary>
        /// Initializes new instance of ElGamal private key with a specified values
        /// </summary>
        /// <param name="binarySize">Binary size used in keys generating</param>
        /// <param name="p">Random prime number P value</param>
        /// <param name="g">Primitive root of P value</param>
        /// <param name="x">Key value X</param>
        public ElGamalPrivateKey(int binarySize, BigInteger p, BigInteger g, BigInteger x)
            : base(binarySize, AlgorithmName.ElGamal, KeyType.Private)
        {
            P = p;
            G = g;
            X = x;
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitElGamalPrivateKey(this);
        }
    }
}
