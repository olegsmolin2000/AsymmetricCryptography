using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels.DSA
{
    internal sealed class DsaKeyShowingViewModel:KeysShowingViewModel
    {
        public string KeyValue { get; set; }

        public DsaDomainParameter DomainParameter { get; set; }
        public DsaDomainParametersShowingViewModel DomainParameterViewModel { get; set; }

        public DsaKeyShowingViewModel(AsymmetricKey key)
            :base(key)
        {
            if (key is DsaPrivateKey)
            {
                KeyValue = (key as DsaPrivateKey).X.ToString();

                int domainParameterId = (int)(key as DsaPrivateKey).DomainParameterId;

                DomainParameter = DataWorker.GetKey(domainParameterId) as DsaDomainParameter;

                DomainParameterViewModel = new DsaDomainParametersShowingViewModel(DomainParameter);
            }
            else if(key is DsaPublicKey)
            {
                KeyValue = (key as DsaPublicKey).Y.ToString();

                int domainParameterId = (int)(key as DsaPublicKey).DomainParameterId;

                DomainParameter = DataWorker.GetKey(domainParameterId) as DsaDomainParameter;

                DomainParameterViewModel = new DsaDomainParametersShowingViewModel(DomainParameter);
            }
            else
                MessageBox.Show("Не DSA ключ!");
        }

        public RelayCommand OpenDPShowingWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window window = new DsaDomainParametersShowingWindow(DomainParameter);

                  window.Show();
              });
        }
    }
}
