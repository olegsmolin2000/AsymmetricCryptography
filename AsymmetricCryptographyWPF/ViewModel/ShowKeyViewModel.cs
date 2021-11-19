using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel
{
    class ShowKeyViewModel:ViewModel
    {
        #region Properties
        private string name;
        public string Name
        {
            get => name;

            set
            {
                name = value;

                NotifyPropertyChanged("Name");
            }
        }

        private string numberGenerator;
        public string NumberGenerator
        {
            get => numberGenerator;

            set
            {
                numberGenerator = value;

                NotifyPropertyChanged("NumberGenerator");
            }
        }

        private string primalityVerificator;
        public string PrimalityVerificator
        {
            get => primalityVerificator;

            set
            {
                primalityVerificator = value;

                NotifyPropertyChanged("PrimalityVerificator");
            }
        }

        private string hashAlgorithm;
        public string HashAlgorithm
        {
            get => hashAlgorithm;

            set
            {
                hashAlgorithm = value;

                NotifyPropertyChanged("HashAlgorithm");
            }
        }

        private string algorithmName;
        public string AlgorithmName
        {
            get => algorithmName;

            set
            {
                algorithmName = value;

                NotifyPropertyChanged("AlgorithmName");
            }
        }

        private string permission;
        public string Permission
        {
            get => permission;

            set
            {
                permission = value;

                NotifyPropertyChanged("Permission");
            }
        }

        private int binarySize;
        public int BinarySize
        {
            get => binarySize;

            set
            {
                binarySize = value;

                NotifyPropertyChanged("BinarySize");
            }
        }

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

        public ShowKeyViewModel(AsymmetricKey key)
        {
            if (!(key is RsaPrivateKey) && (!(key is RsaPublicKey)))
                MessageBox.Show("pezdec");
            else
            {
                Name = key.Name;
                BinarySize = key.BinarySize;
                AlgorithmName = key.AlgorithmName;
                NumberGenerator = key.NumberGenerator;
                PrimalityVerificator = key.PrimalityVerificator;
                HashAlgorithm = key.HashAlgorithm;
                Permission = key.Type;

                if (key is RsaPrivateKey)
                {
                    Modulus = ((RsaPrivateKey)key).Modulus.ToString();
                    Exponent = ((RsaPrivateKey)key).PrivateExponent.ToString();
                }
                if (key is RsaPublicKey)
                {
                    Modulus = ((RsaPublicKey)key).Modulus.ToString();
                    Exponent = ((RsaPublicKey)key).PublicExponent.ToString();
                }
            }
        }

        //private RelayCommand loadData;

        //public RelayCommand LoadData
        //{
        //    get
        //    {
        //        return loadData ?? new RelayCommand(obj =>
        //        {
        //            if (!(key is RsaPrivateKey) && (!(key is RsaPublicKey)))
        //                MessageBox.Show("pezdec");
        //            else
        //            {
        //                Name = key.Name;
        //                BinarySize = key.BinarySize;
        //                AlgorithmName = key.AlgorithmName;
        //                NumberGenerator = key.NumberGenerator;
        //                PrimalityVerificator = key.PrimalityVerificator;
        //                HashAlgorithm = key.HashAlgorithm;
        //                Permission = key.Type;

        //                if (key is RsaPrivateKey)
        //                {
        //                    Modulus = ((RsaPrivateKey)key).Modulus.ToString();
        //                    Exponent = ((RsaPrivateKey)key).PrivateExponent.ToString();
        //                }
        //                if (key is RsaPublicKey)
        //                {
        //                    Modulus = ((RsaPublicKey)key).Modulus.ToString();
        //                    Exponent = ((RsaPublicKey)key).PublicExponent.ToString();
        //                }
        //            }
        //        });
        //    }
        //}

    }
}
