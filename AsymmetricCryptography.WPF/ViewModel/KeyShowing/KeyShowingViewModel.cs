using AsymmetricCryptography.DataUnits.Keys;

namespace AsymmetricCryptography.WPF.ViewModel.KeyShowing
{
    internal abstract class KeyShowingViewModel:ViewModel
    {
        public string Name { get; set; }
        public string AlgorithmName { get; set; }
        public string NumberGenerator { get; set; }
        public string PrimalityVerificator { get; set; }
        public string HashAlgorithm { get; set; }
        public string Permission { get; set; }
        public int BinarySize { get; set; }

        protected KeyShowingViewModel(AsymmetricKey key)
        {
            Name = key.Name;
            AlgorithmName = key.AlgorithmName.ToString();
            NumberGenerator = key.NumberGenerator.ToString();
            PrimalityVerificator = key.PrimalityVerificator.ToString();
            HashAlgorithm = key.HashAlgorithm.ToString();
            Permission = key.KeyType.ToString();
            BinarySize = key.BinarySize;
        }
    }
}
