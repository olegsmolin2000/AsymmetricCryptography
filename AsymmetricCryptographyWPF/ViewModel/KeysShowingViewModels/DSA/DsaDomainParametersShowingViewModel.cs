using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;

namespace AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels.DSA
{
    class DsaDomainParametersShowingViewModel : KeysShowingViewModel
    {
        #region Properties
        private string q;

        public string Q
        {
            get => q;
            set 
            { 
                q = value;

                NotifyPropertyChanged("Q");
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

        public DsaDomainParametersShowingViewModel(AsymmetricKey key) 
            : base(key)
        {
            DsaDomainParameter domainParameter = key as DsaDomainParameter;

            Q = domainParameter.Q.ToString();
            P = domainParameter.P.ToString();
            G = domainParameter.G.ToString();
        }
    }
}
