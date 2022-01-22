using AsymmetricCryptography.Core.KeysGenerators;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeysGenerating
{
    internal sealed class ElGamalKeysGeneratingViewModel:KeysGeneratingViewModel
    {
        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
            {
                if (TryReadProperties())
                {
                    KeysGenerator keysGenerator = new ElGamalKeysGenerator(selectedNumberGenerator, selectedPrimalityTest, selectedHashAlgorithm);
                    
                    Generate(keysGenerator);

                    CloseWindow(obj as Window);
                }
            });
        }
    }
}
