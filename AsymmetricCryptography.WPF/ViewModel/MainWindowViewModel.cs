using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using AsymmetricCryptography.DataUnits.Keys.RSA;
using AsymmetricCryptography.EFCore.Repositories;
using AsymmetricCryptography.WPF.View.KeysGeneratingWindows;
using AsymmetricCryptography.WPF.View.KeyShowingWindows;
using System.Collections.Generic;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel
{
    internal class MainWindowViewModel:ViewModel
    {
        #region KeysRepository
        private readonly KeysRepository<AsymmetricKey> _keysRepository;

        private List<AsymmetricKey> keys;
        public List<AsymmetricKey> Keys
        {
            get => keys;

            set
            {
                keys = value;

                NotifyPropertyChanged(nameof(Keys));
            }
        }

        public RelayCommand LoadKeys
        {
            get => new RelayCommand(obj =>
              {
                  Keys = _keysRepository.Items;
              });
        }

        private AsymmetricKey selectedKey;
        public AsymmetricKey SelectedKey
        {
            get => selectedKey;

            set
            {
                selectedKey = value;

                NotifyPropertyChanged(nameof(SelectedKey));
            }
        }
        #endregion

        #region OpenWindows
        public RelayCommand OpenKeyShowingWindow
        {
            get => new RelayCommand(obj =>
            {
                if (SelectedKey is null)
                    MessageBox.Show("Выберите ключ!");
                else
                {
                    Window keyShowingWnd;

                    if (SelectedKey is RsaPrivateKey || SelectedKey is RsaPublicKey)
                    {
                        keyShowingWnd = new RsaKeyShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                    else if (SelectedKey is ElGamalPrivateKey || SelectedKey is ElGamalPublicKey)
                    {
                        keyShowingWnd = new ElGamalKeyShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                    else if(SelectedKey is DsaDomainParameter)
                    {
                        keyShowingWnd = new DsaDomainParameterShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                    else if(SelectedKey is DsaPrivateKey || SelectedKey is DsaPublicKey)
                    {
                        keyShowingWnd = new DsaKeyShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                }
            });
        }

        public RelayCommand OpenRsaKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window rsaKeysGenerationWindow = new RsaKeysGeneratingWindow();

                rsaKeysGenerationWindow.Owner = Application.Current.MainWindow;

                rsaKeysGenerationWindow.Show();
            });
        }

        public RelayCommand OpenElGamalKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window elGamalKeysGenerationWindow = new ElGamalKeysGeneratingWindow();

                elGamalKeysGenerationWindow.Owner = Application.Current.MainWindow;

                elGamalKeysGenerationWindow.Show();
            });
        }

        public RelayCommand OpenDsaDomainParameterGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window dsaDPGeneratingWnd = new DsaDomainParametersGeneratingWindow();

                dsaDPGeneratingWnd.Owner = Application.Current.MainWindow;

                dsaDPGeneratingWnd.Show();
            });
        }

        public RelayCommand OpenDsaKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window dsaKeysGeneratingWnd = new DsaKeysGeneratingWindow();

                dsaKeysGeneratingWnd.Owner = Application.Current.MainWindow;

                dsaKeysGeneratingWnd.Show();
            });
        }

        public RelayCommand OpenDsaKeysGeneratingByDPWindow
        {
            get => new RelayCommand(obj =>
             {
                 Window dsaKeysGeneratingWnd = new DsaKeysGeneratingByDPWindow();

                 dsaKeysGeneratingWnd.Owner = Application.Current.MainWindow;

                 dsaKeysGeneratingWnd.Show();
             });
        }
        #endregion

        public MainWindowViewModel()
        {
            _keysRepository = new KeysRepository<AsymmetricKey>();
        }
    }
}
