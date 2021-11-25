using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA
{
    /// <summary>
    /// Логика взаимодействия для DsaKeyShowingWindow.xaml
    /// </summary>
    public partial class DsaKeyShowingWindow : Window
    {
        public DsaKeyShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new DsaKeyShowingViewModel(key);
        }
    }
}
