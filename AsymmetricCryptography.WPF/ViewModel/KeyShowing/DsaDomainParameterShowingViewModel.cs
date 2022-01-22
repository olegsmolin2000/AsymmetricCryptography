using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;

namespace AsymmetricCryptography.WPF.ViewModel.KeyShowing
{
    internal class DsaDomainParameterShowingViewModel:KeyShowingViewModel
    {
        public string Q { get; set; }
        public string P { get; set; }
        public string G { get; set; }

        public DsaDomainParameterShowingViewModel(AsymmetricKey key)
            : base(key)
        {
            DsaDomainParameter domainParameter = key as DsaDomainParameter;

            Q = domainParameter.Q.ToString();
            P = domainParameter.P.ToString();
            G = domainParameter.G.ToString();
        }
    }
}
