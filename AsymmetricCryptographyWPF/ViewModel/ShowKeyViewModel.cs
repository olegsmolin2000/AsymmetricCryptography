using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel
{
    internal abstract class ShowKeyViewModel : ViewModel
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
        #endregion

        public ShowKeyViewModel(AsymmetricKey key)
        {
            Name = key.Name;
            AlgorithmName = key.AlgorithmName;
            NumberGenerator = key.NumberGenerator;
            PrimalityVerificator = key.PrimalityVerificator;
            HashAlgorithm = key.HashAlgorithm;
            Permission = key.Type;
            BinarySize = key.BinarySize;
        }
    }
}
