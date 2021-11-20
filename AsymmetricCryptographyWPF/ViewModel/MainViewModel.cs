using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using System.Windows;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using AsymmetricCryptographyWPF.View.ShowKeyWindows;

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

        public AsymmetricKey SelectedKey { get; set; }

        public MainViewModel()
        {
            allKeys = DataWorker.GetAll();
        }

        public RelayCommand OpenShowKeyWindow
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedKey is RsaPrivateKey || SelectedKey is RsaPublicKey)
                    {
                        Window showKeyWindow = new ShowRsaKeyWindow(SelectedKey);

                        showKeyWindow.Show();
                    }
                });
            }
        }

    }
}
