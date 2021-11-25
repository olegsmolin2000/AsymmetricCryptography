using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeyShowingWindows
{
    /// <summary>
    /// Логика взаимодействия для RsaKeyShowingWindow.xaml
    /// </summary>
    public partial class RsaKeyShowingWindow : Window
    {
        public RsaKeyShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new RsaKeysShowingViewModel(key);
        }
    }
}
