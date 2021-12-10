namespace AsymmetricCryptography.DataUnits.Keys
{
    public abstract class AsymmetricKey
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int BinarySize { get; private init; }
        public AlgorithmName AlgorithmName { get; private init; }
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
    }

    public enum AlgorithmName
    {
        RSA,
        DSA,
        ElGamal
    }

    public enum KeyType
    {
        Private,
        Public,
        DomainParameter
    }
}