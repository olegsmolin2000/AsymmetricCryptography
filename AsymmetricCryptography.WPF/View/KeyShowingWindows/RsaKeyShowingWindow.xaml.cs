using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.WPF.ViewModel.KeyShowing;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeyShowingWindows
{
    /// <summary>
    /// Логика взаимодействия для RsaKeyShowingWindow.xaml
    /// </summary>
    public partial class RsaKeyShowingWindow : Window
    {
        public RsaKeyShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new RsaKeyShowingViewModel(key);
        }
    }
}
