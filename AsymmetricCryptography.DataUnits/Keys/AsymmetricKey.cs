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

        // TODO: Переделать, чтобы было легкое конвертирование в
        // объекты классов NumberGenerator, PrimalityVerificator, HashAlgorithm
        // в обе стороны, и эти свойства были иммутабельные, возможно init setters
        public string? NumberGenerator { get; set; }
        public string? PrimalityVerificator { get; set; }
        public string? HashAlgorithm { get; set; }

        protected AsymmetricKey(int binarySize, AlgorithmName algorithmName, KeyType keyType)
        {
            BinarySize = binarySize;
            AlgorithmName = algorithmName;
            KeyType = keyType;
        }

        public abstract void Accept(IKeyVisitor keyVisitor);
    }
}