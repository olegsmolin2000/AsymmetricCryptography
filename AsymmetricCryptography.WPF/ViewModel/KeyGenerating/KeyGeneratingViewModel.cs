using AsymmetricCryptography.DataUnits;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeyGenerating
{
    internal abstract class KeyGeneratingViewModel:ViewModel
    {
        protected static KeysRepository<AsymmetricKey> Repository { get; }

        #region ListsForComboboxes
        public static readonly string[] NumberGenerators;
        public static readonly string[] PrimalityVerificators;
        public static readonly string[] HashAlgorithms;
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

        private RandomNumberGenerator selectedNumberGenerator;
        public RandomNumberGenerator SelectedNumberGenerator
        {
            get => selectedNumberGenerator;

            set 
            { 
                selectedNumberGenerator = value;

                NotifyPropertyChanged("SelectedNumberGenerator");
            }
        }

        private PrimalityTest selectedPrimalityTest;
        public PrimalityTest SelectedPrimalityTest
        {
            get => selectedPrimalityTest;

            set
            {
                selectedPrimalityTest = value;

                NotifyPropertyChanged("SelectedPrimalityTest");
            }
        }

        private CryptographicHashAlgorithm selectedHashAlgorithm;
        public CryptographicHashAlgorithm SelectedHashAlgorithm
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

        static KeyGeneratingViewModel()
        {
            Repository = new KeysRepository<AsymmetricKey>();

            List<string> numberGenerators = new List<string>();
            
            foreach (string numberGenerator in Enum.GetNames(typeof(RandomNumberGenerator)))
            {
                numberGenerators.Add(numberGenerator);
            }

            NumberGenerators = numberGenerators.ToArray();

            List<string> primalityVerificators = new List<string>();

            foreach (string primalityVerificator in Enum.GetNames(typeof(PrimalityTest)))
            {
                primalityVerificators.Add(primalityVerificator);
            }

            PrimalityVerificators = primalityVerificators.ToArray();

            List<string> hashAlgorithms = new List<string>();

            foreach (string hashAlgorithm in Enum.GetNames(typeof(CryptographicHashAlgorithm)))
            {
                hashAlgorithms.Add(hashAlgorithm);
            }

            HashAlgorithms = hashAlgorithms.ToArray();
        }

        protected AsymmetricKey privateKey, publicKey;

        public abstract RelayCommand GenerateKeys { get; }

        protected bool TryReadProperties()
        {
            if (Name == null || Name.Replace(" ", "").Length == 0)
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
                if (Repository.Items.Exists(key => key.Name == Name))
                {
                    MessageBox.Show("Ключи с таким названием уже существуют!");

                    return false;
                }
                else
                {
                    if (SelectedNumberGenerator == null)
                        return false;

                    if (SelectedPrimalityTest == null)
                        return false;

                    if (SelectedHashAlgorithm == null)
                        return false;

                    MessageBox.Show("Выберите параметры для генерации!");
                }
            }

            return true;
        }

        protected void FillDB()
        {
            if (privateKey != null && publicKey != null)
            {
                Repository.Add(privateKey);
                Repository.Add(publicKey);
            }
            else
            {
                MessageBox.Show("Ошибка. Ключи пустые!");
            }
        }

        protected void CloseWindow(Window window)
        {
            MessageBox.Show("Ключи успешно созданы!");

            ((MainWindowViewModel)window.Owner.DataContext).LoadKeys.Execute(null);

            window.Owner.Activate();

            window.Close();
        }
    }
}
