using AsymmetricCryptography.Core.KeysGenerators;
using AsymmetricCryptography.DataUnits;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeysGenerating
{
    internal abstract class KeysGeneratingViewModel : ViewModel
    {
        protected static KeysRepository<AsymmetricKey> Repository { get; }

        #region ListsForComboboxes
        public static List<string> NumberGenerators { get; }
        public static List<string> PrimalityVerificators { get; }
        public static List<string> HashAlgorithms { get; }
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

        protected RandomNumberGenerator selectedNumberGenerator;
        public string SelectedNumberGenerator
        {
            get => selectedNumberGenerator.ToString();

            set
            {
                Enum.TryParse(value,out selectedNumberGenerator);

                NotifyPropertyChanged("SelectedNumberGenerator");
            }
        }

        protected PrimalityTest selectedPrimalityTest;
        public string SelectedPrimalityTest
        {
            get => selectedPrimalityTest.ToString();

            set
            {
                Enum.TryParse(value, out selectedPrimalityTest);

                NotifyPropertyChanged("SelectedPrimalityTest");
            }
        }

        protected CryptographicHashAlgorithm selectedHashAlgorithm;
        public string SelectedHashAlgorithm
        {
            get => selectedHashAlgorithm.ToString();

            set
            {
                Enum.TryParse(value, out selectedHashAlgorithm);

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

        public abstract RelayCommand GenerateKeys { get; }

        static KeysGeneratingViewModel()
        {
            Repository = new KeysRepository<AsymmetricKey>();

            NumberGenerators = new List<string>();

            foreach (string numberGenerator in Enum.GetNames(typeof(RandomNumberGenerator)))
            {
                NumberGenerators.Add(numberGenerator);
            }

            PrimalityVerificators = new List<string>();

            foreach (string primalityVerificator in Enum.GetNames(typeof(PrimalityTest)))
            {
                PrimalityVerificators.Add(primalityVerificator);
            }

            HashAlgorithms = new List<string>();

            foreach (string hashAlgorithm in Enum.GetNames(typeof(CryptographicHashAlgorithm)))
            {
                HashAlgorithms.Add(hashAlgorithm);
            }
        }

        public KeysGeneratingViewModel()
        {
            SelectedNumberGenerator = NumberGenerators[0];
        }

        protected AsymmetricKey privateKey, publicKey;

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
                    {
                        MessageBox.Show("Выберите параметры для генерации!");

                        return false;
                    }
                        
                    if (SelectedPrimalityTest == null)
                    {
                        MessageBox.Show("Выберите параметры для генерации!");

                        return false;
                    }

                    if (SelectedHashAlgorithm == null)
                    {
                        MessageBox.Show("Выберите параметры для генерации!");

                        return false;
                    }
                }
            }

            return true;
        }

        protected void Generate(KeysGenerator keysGenerator)
        {
            keysGenerator.GenerateKeys(BinarySize, out privateKey, out publicKey);

            SetParameters(privateKey);
            SetParameters(publicKey);

            Repository.Add(privateKey);
            Repository.Add(publicKey);
        }

        protected void CloseWindow(Window window)
        {
            MessageBox.Show("Ключи успешно созданы!");

            ((MainWindowViewModel)window.Owner.DataContext).LoadKeys.Execute(null);

            window.Owner.Activate();

            window.Close();
        }

        protected void SetParameters(AsymmetricKey key)
        {
            if (key is null)
                throw new NullReferenceException();

            key.NumberGenerator = selectedNumberGenerator;
            key.PrimalityVerificator = selectedPrimalityTest;
            key.HashAlgorithm = selectedHashAlgorithm;

            key.Name = Name;
        }
    }
}