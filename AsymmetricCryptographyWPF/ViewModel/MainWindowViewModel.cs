using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using System.Windows;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using AsymmetricCryptographyWPF.View.KeysGeneratingWindows;
using AsymmetricCryptographyWPF.View.KeyShowingWindows;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels;
using AsymmetricCryptographyWPF.View.KeysGeneratingWindows.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA;

namespace AsymmetricCryptographyWPF.ViewModel
{
    class MainWindowViewModel:ViewModel
    {
        private List<AsymmetricKey> allKeys;

        public List<AsymmetricKey> AllKeys
        {
            get => allKeys;

            set 
            { 
                allKeys = value;
                NotifyPropertyChanged("AllKeys");
            }
        }

        public void RefreshData()
        {
            LoadKeys.Execute(null);
        }

        public RelayCommand LoadKeys
        {
            get => new RelayCommand(obj =>
            {
                AllKeys = DataWorker.GetAll();
            });
        }


        #region SelectedKey
        private KeysShowingViewModel selectedKeyShowingVM;
        public KeysShowingViewModel SelectedKeyShowingVM
        {
            get => selectedKeyShowingVM;

            set
            {
                selectedKeyShowingVM = value;

                NotifyPropertyChanged("SelectedKeyShowingVM");
            }
        }


        private AsymmetricKey selectedKey;
        public AsymmetricKey SelectedKey
        {
            get => selectedKey;

            set
            {
                selectedKey = value;

                NotifyPropertyChanged("SelectedKey");

                SelectedKeyShowingVM = new KeysShowingViewModel(SelectedKey); 
            }
        }
        #endregion

        #region TextFields
        private string inputedText = "zhopa";
        public string InputedText
        {
            get => inputedText;

            set
            {
                inputedText = value;

                NotifyPropertyChanged("InputedText");
            }
        }

        private string resultText = "pizda";

        public string ResultText
        {
            get => resultText;
            set
            {
                resultText = value;

                NotifyPropertyChanged("ResultText");
            }
        }
        #endregion

        #region OpenWindows
        public RelayCommand OpenShowKeyWindow
        {
            get => new RelayCommand(obj =>
            {
                Window window;

                if (SelectedKey is RsaPrivateKey || SelectedKey is RsaPublicKey)

                    window = new RsaKeyShowingWindow(SelectedKey);
                else if (selectedKey is DsaDomainParameter)
                    window = new DsaDomainParametersShowingWindow(SelectedKey);
                else
                    window = new DsaKeyShowingWindow(selectedKey);

                window.Show();
            });
        }

        public RelayCommand OpenGenerateRsaKeysWindow
        {
            get => new RelayCommand(obj =>
            {
                Window rsaKeysGenerationWindow = new RsaKeysGeneratingWindow();

                rsaKeysGenerationWindow.Owner = Application.Current.MainWindow;

                rsaKeysGenerationWindow.Show();
            });
        }

        public RelayCommand OpenDsaDPGeneratingWindow
        {
            get => new RelayCommand(obj =>
             {
                 Window generatingWindow = new DsaDomainParameterGeneratingWindow();

                 generatingWindow.Owner = Application.Current.MainWindow;

                 generatingWindow.Show();
             });
        }

        public RelayCommand OpenDsaKeysGeneratingByDPWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window generatingWindow = new DsaKeysGeneratingByDPWindow();

                  generatingWindow.Owner = Application.Current.MainWindow;

                  generatingWindow.Show();
              });
        }

        public RelayCommand OpenDsaKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window generatingWindow = new DsaKeysGeneratingWindow();

                  generatingWindow.Owner = Application.Current.MainWindow;

                  generatingWindow.Show();
              });
        }
        #endregion
    }
}
