using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows;
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
                    if (SelectedKey != null)
                    {
                        Window showKeyWindow = new ShowKeyWindow(SelectedKey);

                        showKeyWindow.Show();
                    }
                });
            }
        }

    }
}
