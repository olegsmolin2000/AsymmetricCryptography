using AsymmetricCryptography;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel
{
    internal abstract class KeysGeneratingViewModel:ViewModel
    {
        #region ListsForComboBoxes
        private List<string> numberGenerators = new List<string> { "Lagged Fibonacci", "Blum Blum Shub" };
        public List<string> NumberGenerators
        {
            get => numberGenerators;

            set
            {
                numberGenerators = value;

                NotifyPropertyChanged("NumberGenerators");
            }
        }

        private List<string> primalityVerificators = new List<string> { "Miller-Rabin" };
        public List<string> PrimalityVerificators
        {
            get => primalityVerificators;

            set
            {
                primalityVerificators = value;

                NotifyPropertyChanged("PrimalityVerificators");
            }
        }

        private List<string> hashAlgorithmNames = new List<string> { "SHA-256", "MD-5" };
        public List<string> HashAlgorithmNames
        {
            get => hashAlgorithmNames;

            set
            {
                hashAlgorithmNames = value;

                NotifyPropertyChanged("HashAlgorithmNames");
            }
        }
        #endregion

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

        private string selectedNumberGenerator;
        public string SelectedNumberGenerator
        {
            get => selectedNumberGenerator;
            set
            {
                selectedNumberGenerator = value;

                NotifyPropertyChanged("SelectedNumberGenerator");
            }
        }

        private string selectedPrimalityVerificator;
        public string SelectedPrimalityVerificator
        {
            get => selectedPrimalityVerificator;

            set
            {
                selectedPrimalityVerificator = value;

                NotifyPropertyChanged("SelectedPrimalityVerificator");
            }
        }

        private string selectedHashAlgorithm;
        public string SelectedHashAlgorithm
        {
            get => selectedHashAlgorithm;

            set
            {
                selectedHashAlgorithm = value;

                NotifyPropertyChanged("SelectedHashAlgorithm");
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

        public KeysGeneratingViewModel()
        {
            SelectedNumberGenerator = numberGenerators[0];
            SelectedPrimalityVerificator = primalityVerificators[0];
            SelectedHashAlgorithm = hashAlgorithmNames[0];
        }

        protected GeneratingParameters generationParameters;

        protected AsymmetricKey privateKey, publicKey;

        public abstract RelayCommand GenerateKeys { get; }

        protected bool TryReadProperties()
        {
            if (name == null || name.Replace(" ", "").Length == 0)
            {
                MessageBox.Show("Введите название ключей!");

                return false;
            }
            else if (binarySize <= 8 || binarySize > 4096)
            {
                MessageBox.Show("Размер ключей должен быть от 8 до 4096!");

                return false;
            }
            else
            {
                if (DataWorker.ContainsKey(name))
                {
                    MessageBox.Show("Ключи с таким названием уже существуют!");

                    return false;
                }
                else
                {
                    string[] genParameters = new string[3];

                    genParameters[0] = selectedNumberGenerator;
                    genParameters[1] = selectedPrimalityVerificator;
                    genParameters[2] = selectedHashAlgorithm;

                    generationParameters = GeneratingParameters.GetParametersByInfo(genParameters);
                }
            }

            return true;
        }

        protected void FillDBAndClose(Window window)
        {
            if (privateKey != null && publicKey != null)
            {
                DataWorker.AddKey(privateKey);
                DataWorker.AddKey(publicKey);
            }

            CloseWindow(window);
        }

        protected void CloseWindow(Window window)
        {
            MessageBox.Show("Ключи успешно созданы!");

            ((MainWindowViewModel)window.Owner.DataContext).RefreshData();

            window.Owner.Activate();

            window.Close();
        }
    }
}
