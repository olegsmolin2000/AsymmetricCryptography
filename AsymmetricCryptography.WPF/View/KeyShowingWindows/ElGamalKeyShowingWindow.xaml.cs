using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.WPF.ViewModel.KeyShowing;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeyShowingWindows
{
    /// <summary>
    /// Логика взаимодействия для ElGamalKeyShowingWindow.xaml
    /// </summary>
    public partial class ElGamalKeyShowingWindow : Window
    {
        public ElGamalKeyShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new ElGamalKeyShowingViewModel(key);
        }
    }
}
