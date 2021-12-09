namespace AsymmetricCryptography.DataUnits.Keys
{
    public abstract class AsymmetricKey
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AlgorithmName AlgorithmName { get; private set; }
        public KeyType KeyType { get; private set; }
        public int BinarySize { get; private set; }

        // TODO: Переделать, чтобы было легкое конвертирование в
        // объекты классов NumberGenerator, PrimalityVerificator, HashAlgorithm
        // в обе стороны, и эти свойства были иммутабельные
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