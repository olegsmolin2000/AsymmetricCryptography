using AsymmetricCryptography.Core.KeysGenerators;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeysGenerating
{
    internal sealed class RsaKeysGeneratingViewModel:KeysGeneratingViewModel
    {
        private bool isFixedPublicExponent = true;
        public bool IsFixedPublicExponent
        {
            get => isFixedPublicExponent;

            set
            {
                isFixedPublicExponent = value;

                NotifyPropertyChanged("IsFixedPublicExponent");
            }
        }

        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
            {
                if (TryReadProperties())
                {
                    RsaKeysGenerator keysGenerator = new RsaKeysGenerator(selectedNumberGenerator, selectedPrimalityTest, selectedHashAlgorithm);

                    keysGenerator.FixedPublicExponent = IsFixedPublicExponent;

                    Generate(keysGenerator);

                    CloseWindow(obj as Window);
                }
            });
        }
    }
}
