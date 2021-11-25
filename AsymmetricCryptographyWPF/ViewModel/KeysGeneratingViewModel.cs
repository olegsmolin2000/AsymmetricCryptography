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
        private List<string> numberGenerators = new List<string> { "Фибоначчи" };
        public List<string> NumberGenerators
        {
            get => numberGenerators;

            set
            {
                numberGenerators = value;

                NotifyPropertyChanged("NumberGenerators");
            }
        }

        private List<string> primalityVerificators = new List<string> { "Миллера-рабина" };
        public List<string> PrimalityVerificators
        {
            get => primalityVerificators;

            set
            {
                primalityVerificators = value;

                NotifyPropertyChanged("PrimalityVerificators");
            }
        }

        private List<string> hashAlgorithmNames = new List<string> { "Sha-256", "MD-5" };
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

        protected Parameters generationParameters;
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
                    PrimalityVerificator primality = new MillerRabinPrimalityVerificator();

                    NumberGenerator numberGenerator = new FibonacciNumberGenerator(primality);

                    CryptographicHashAlgorithm hashAlgorithm;

                    switch (SelectedHashAlgorithm)
                    {
                        case "Sha-256":
                            {
                                hashAlgorithm = new SHA_256();
                                break;
                            }
                        default:
                            {
                                hashAlgorithm = new MD_5();
                                break;
                            }
                    }

                    generationParameters = new Parameters(numberGenerator, primality, hashAlgorithm);
                }
            }

            return true;
        }

        protected void FillDBAndClose(Window window)
        {
            if (generationParameters != null)
            {
                string[] paramsInfo = generationParameters.GetParameters();

                privateKey.NumberGenerator = paramsInfo[0];
                publicKey.NumberGenerator = paramsInfo[0];

                privateKey.PrimalityVerificator = paramsInfo[1];
                publicKey.PrimalityVerificator = paramsInfo[1];

                privateKey.HashAlgorithm = paramsInfo[2];
                publicKey.HashAlgorithm = paramsInfo[2];
            }

            if (privateKey != null && publicKey != null)
            {
                DataWorker.AddKey(privateKey);
                DataWorker.AddKey(publicKey);
            }

            MessageBox.Show("Ключи успешно созданы!");

            ((MainWindowViewModel)window.Owner.DataContext).RefreshData();

            window.Owner.Activate();

            window.Close();
        }
    }
}
