using AsymmetricCryptography.Core.KeysGenerators;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeysGenerating
{
    internal sealed class DsaDomainParametersGeneratingViewModel:KeysGeneratingViewModel
    {
        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
            {
                if (TryReadProperties())
                {
                    DsaKeysGenerator keysGenerator = new DsaKeysGenerator(selectedNumberGenerator, selectedPrimalityTest, selectedHashAlgorithm);

                    int l = BinarySize;
                    int n = keysGenerator.DigestBitLength;

                    if (l <= n)
                        MessageBox.Show("Размер ключа должен быть больше длины хеша!");

                    DsaDomainParameter domainParameter = keysGenerator.DsaDomainParametersGeneration(l, n);

                    SetParameters(domainParameter);

                    Repository.Add(domainParameter);

                    CloseWindow(obj as Window);
                }
            });
        }
    }
}
