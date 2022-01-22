using AsymmetricCryptography.Core.KeysGenerators;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.EFCore.Repositories;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeysGenerating
{
    internal sealed class DsaKeysGeneratingViewModel:KeysGeneratingViewModel
    {
        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
            {
                if (TryReadProperties())
                {
                    DsaKeysGenerator keysGenerator = new DsaKeysGenerator(selectedNumberGenerator, selectedPrimalityTest, selectedHashAlgorithm);

                    DsaDomainParameter domainParameter;

                    int l = BinarySize;
                    int n = keysGenerator.DigestBitLength;

                    if (l <= n)
                        MessageBox.Show("Размер ключа должен быть больше длины хеша!");

                    domainParameter = keysGenerator.DsaDomainParametersGeneration(l, n);
                    SetParameters(domainParameter);
                    Repository.Add(domainParameter);

                    KeysRepository<DsaDomainParameter> domainParametersRepository = new KeysRepository<DsaDomainParameter>();
                    domainParameter = domainParametersRepository.Get(Name);

                    keysGenerator.DsaKeysGeneration(domainParameter, out privateKey, out publicKey);

                    SetParameters(privateKey);
                    SetParameters(publicKey);

                    Repository.Add(privateKey);
                    Repository.Add(publicKey);

                    CloseWindow(obj as Window);
                }
            });
        }
    }
}
