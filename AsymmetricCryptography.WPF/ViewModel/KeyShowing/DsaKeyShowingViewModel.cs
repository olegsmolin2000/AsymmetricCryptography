using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.WPF.View.KeyShowingWindows;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeyShowing
{
    internal class DsaKeyShowingViewModel:KeyShowingViewModel
    {
        public string KeyValue { get; set; }

        public DsaDomainParameter DomainParameter { get; set; }
        public DsaDomainParameterShowingViewModel DomainParameterViewModel { get; set; }

        public DsaKeyShowingViewModel(AsymmetricKey key)
            : base(key)
        {
            if (key is DsaPrivateKey)
            {
                DsaPrivateKey privateKey = key as DsaPrivateKey;

                KeyValue = privateKey.X.ToString();

                DomainParameter = privateKey.DomainParameter;

                DomainParameterViewModel = new DsaDomainParameterShowingViewModel(DomainParameter);
            }
            else if (key is DsaPublicKey)
            {
                DsaPublicKey publicKey = key as DsaPublicKey;

                KeyValue = publicKey.Y.ToString();

                DomainParameter = publicKey.DomainParameter;

                DomainParameterViewModel = new DsaDomainParameterShowingViewModel(DomainParameter);
            }
            else
                MessageBox.Show("Не DSA ключ!");
        }

        public RelayCommand OpenDPShowingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window window = new DsaDomainParameterShowingWindow(DomainParameter);

                window.Show();
            });
        }
    }
}
