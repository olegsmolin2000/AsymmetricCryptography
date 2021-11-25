using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA
{
    /// <summary>
    /// Логика взаимодействия для DsaDomainParametersShowingWindow.xaml
    /// </summary>
    public partial class DsaDomainParametersShowingWindow : Window
    {
        public DsaDomainParametersShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new DsaDomainParametersShowingViewModel(key);
        }
    }
}
