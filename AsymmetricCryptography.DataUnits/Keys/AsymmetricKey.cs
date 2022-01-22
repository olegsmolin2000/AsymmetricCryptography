namespace AsymmetricCryptography.DataUnits.Keys
{
    /// <summary>
    /// Base class for asymmetric cryptographic keys. Contains all generating parameters
    /// </summary>
    public abstract class AsymmetricKey
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        /// <summary>
        /// Binary size used in keys generating
        /// </summary>
        public int BinarySize { get; private init; }

        /// <summary>
        /// Name of cryprographic algorithm
        /// </summary>
        public AlgorithmName AlgorithmName { get; private init; }

        /// <summary>
        /// Permission type of key
        /// </summary>
        public KeyType KeyType { get; private init; }

        public RandomNumberGenerator NumberGenerator { get; set; }
        public PrimalityTest PrimalityVerificator { get; set; }
        public CryptographicHashAlgorithm HashAlgorithm { get; set; }

        protected AsymmetricKey(int binarySize, AlgorithmName algorithmName, KeyType keyType)
        {
            BinarySize = binarySize;
            AlgorithmName = algorithmName;
            KeyType = keyType;
        }

        public abstract void Accept(IKeyVisitor keyVisitor);
    }
}