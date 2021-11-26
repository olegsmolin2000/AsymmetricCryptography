using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels
{
    internal sealed class ElGamalKeysShowingViewModel: KeysShowingViewModel
    {
        #region Properties
        private string keyValue;
        public string KeyValue
        {
            get => keyValue;

            set
            {
                keyValue = value;

                NotifyPropertyChanged("KeyValue");
            }
        }

        private string p;
        public string P
        {
            get => p;

            set
            {
                p = value;

                NotifyPropertyChanged("P");
            }
        }

        private string g;
        public string G
        {
            get => g;

            set
            {
                g = value;

                NotifyPropertyChanged("G");
            }
        }
        #endregion

        public ElGamalKeysShowingViewModel(AsymmetricKey key)
            : base(key)
        {
            if (key is ElGamalPrivateKey)
            {
                keyValue = (key as ElGamalPrivateKey).X.ToString();
                P = (key as ElGamalPrivateKey).P.ToString();
                G = (key as ElGamalPrivateKey).G.ToString();
            }
            else if (key is ElGamalPublicKey)
            {
                keyValue = (key as ElGamalPublicKey).Y.ToString();
                P = (key as ElGamalPublicKey).P.ToString();
                G = (key as ElGamalPublicKey).G.ToString();
            }
            else
                MessageBox.Show("Не ElGamal ключ!");
        }
    }
}
