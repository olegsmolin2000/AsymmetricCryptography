using AsymmetricCryptography;
using AsymmetricCryptography.ElGamal;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels
{
    internal sealed class ElGamalKeysGeneratingViewModel : KeysGeneratingViewModel
    {
        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
              {
                  if (TryReadProperties())
                  {
                      KeysGenerator keysGenerator = new ElGamalKeysGenerator(generationParameters);

                      keysGenerator.GenerateKeyPair(Name, BinarySize, out privateKey, out publicKey);

                      FillDBAndClose(obj as Window);
                  }
              });
        }
    }
}
