using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.ShowKeyViewModels
{
    internal sealed class ShowRsaKeyViewModel : ShowKeyViewModel
    {
        #region Properties
        private string modulus;
        public string Modulus
        {
            get => modulus;

            set
            {
                modulus = value;

                NotifyPropertyChanged("Modulus");
            }
        }

        private string exponent;
        public string Exponent
        {
            get => exponent;

            set
            {
                exponent = value;

                NotifyPropertyChanged("Exponent");
            }
        }
        #endregion

        public ShowRsaKeyViewModel(AsymmetricKey key)
            :base(key)
        {
            if (key is RsaPrivateKey)
            {
                Modulus = (key as RsaPrivateKey).Modulus.ToString();
                Exponent = (key as RsaPrivateKey).PrivateExponent.ToString();
            }
            else if (key is RsaPublicKey)
            {
                Modulus = (key as RsaPublicKey).Modulus.ToString();
                Exponent = (key as RsaPublicKey).PublicExponent.ToString();
            }
            else
                MessageBox.Show("Не RSA ключ!");
        }
    }
}
