using AsymmetricCryptography;
using AsymmetricCryptography.RSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.GenerateKeysViewModels
{
    internal sealed class GenerateRsaKeysViewModel : GenerateKeysViewModel
    {
        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
              {
                  if (TryReadProperties())
                  {
                      KeysGenerator keysGenerator = new RsaKeysGenerator(generationParameters);
                      keysGenerator.GenerateKeyPair(Name, BinarySize, out privateKey, out publicKey);

                      FillDBAndClose(obj as Window);
                  }
              });
        }
    }
}
