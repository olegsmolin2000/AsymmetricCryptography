namespace AsymmetricCryptography.DataUnits.Keys
{
    public abstract class AsymmetricKey
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AlgorithmName AlgorithmName { get; init; }
        public KeyType KeyType { get; init; }
        public int BinarySize { get; init; }

        // TODO: Переделать, чтобы было легкое конвертирование в
        // объекты классов NumberGenerator, PrimalityVerificator, HashAlgorithm
        // в обе стороны, и эти свойства были иммутабельные, возможно init setters
        public string? NumberGenerator { get; set; }
        public string? PrimalityVerificator { get; set; }
        public string? HashAlgorithm { get; set; }

        protected AsymmetricKey(AlgorithmName algorithmName, KeyType keyType, int binarySize)
        {
            AlgorithmName = algorithmName;
            KeyType = keyType;
            BinarySize = binarySize;
        }
    }
}