using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.WPF.ViewModel.KeyShowing;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeyShowingWindows
{
    /// <summary>
    /// Логика взаимодействия для DsaDomainParameterShowingWindow.xaml
    /// </summary>
    public partial class DsaDomainParameterShowingWindow : Window
    {
        public DsaDomainParameterShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new DsaDomainParameterShowingViewModel(key);
        }
    }
}
