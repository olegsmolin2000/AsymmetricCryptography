using AsymmetricCryptography;
using AsymmetricCryptography.RSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels
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
                    RsaKeysGenerator keysGenerator = new RsaKeysGenerator(generationParameters);

                    keysGenerator.IsFixedPublicExponent = IsFixedPublicExponent;

                    keysGenerator.GenerateKeyPair(Name, BinarySize, out privateKey, out publicKey);

                    FillDBAndClose(obj as Window);
                }
            });
        }
    }
}
