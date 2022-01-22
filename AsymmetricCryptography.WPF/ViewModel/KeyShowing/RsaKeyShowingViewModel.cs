using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.RSA;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeyShowing
{
    internal class RsaKeyShowingViewModel : KeyShowingViewModel
    {
        public string Modulus { get; set; }
        public string Exponent { get; set; }

        public RsaKeyShowingViewModel(AsymmetricKey key)
            : base(key)
        {
            if (key is RsaPrivateKey)
            {
                Modulus = ((RsaPrivateKey)key).Modulus.ToString();
                Exponent = ((RsaPrivateKey)key).PrivateExponent.ToString();
            }
            else if (key is RsaPublicKey)
            {
                Modulus = ((RsaPublicKey)key).Modulus.ToString();
                Exponent = ((RsaPublicKey)key).PublicExponent.ToString();
            }
            else
                MessageBox.Show("Не RSA ключ!");
        }
    }
}
