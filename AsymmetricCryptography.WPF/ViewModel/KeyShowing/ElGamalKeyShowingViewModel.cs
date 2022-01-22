using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeyShowing
{
    internal class ElGamalKeyShowingViewModel:KeyShowingViewModel
    {
        public string KeyValue { get; set; }
        public string P { get; set; }
        public string G { get; set; }

        public ElGamalKeyShowingViewModel(AsymmetricKey key)
           : base(key)
        {
            if (key is ElGamalPrivateKey)
            {
                ElGamalPrivateKey privateKey = key as ElGamalPrivateKey;

                KeyValue = privateKey.X.ToString();
                P = privateKey.P.ToString();
                G = privateKey.G.ToString();
            }
            else if (key is ElGamalPublicKey)
            {
                ElGamalPublicKey publicKey = key as ElGamalPublicKey;

                KeyValue = publicKey.Y.ToString();
                P = publicKey.P.ToString();
                G = publicKey.G.ToString();
            }
            else
                MessageBox.Show("Не ElGamal ключ!");
        }
    }
}
