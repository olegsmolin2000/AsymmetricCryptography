using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.WPF.ViewModel.KeyShowing;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeyShowingWindows
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
