using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using System.Windows;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using AsymmetricCryptographyWPF.View.ShowKeyWindows;
using AsymmetricCryptographyWPF.View;

namespace AsymmetricCryptographyWPF.ViewModel
{
    class MainViewModel:ViewModel
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

        public AsymmetricKey SelectedKey { get; set; }

        public RelayCommand OpenShowKeyWindow
        {
            get => new RelayCommand(obj =>
            {
                if (SelectedKey is RsaPrivateKey || SelectedKey is RsaPublicKey)
                {
                    Window showKeyWindow = new ShowRsaKeyWindow(SelectedKey);

                    showKeyWindow.Show();
                }
            });
        }

        public RelayCommand LoadKeys
        {
            get => new RelayCommand(obj =>
            {
                AllKeys = DataWorker.GetAll();
            });
        }

        public RelayCommand OpenGenerateRsaKeysWindow
        {
            get => new RelayCommand(obj =>
             {
                 Window rsaKeysGenerationWindow = new GenerateKeysWindow();

                 rsaKeysGenerationWindow.Owner = Application.Current.MainWindow;

                 rsaKeysGenerationWindow.Show();
             });
        }
    }
}
