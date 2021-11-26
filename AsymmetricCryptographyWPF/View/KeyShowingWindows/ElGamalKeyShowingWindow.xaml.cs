using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeyShowingWindows
{
    /// <summary>
    /// Логика взаимодействия для ElGamalKeyShowingWindow.xaml
    /// </summary>
    public partial class ElGamalKeyShowingWindow : Window
    {
        public ElGamalKeyShowingWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new ElGamalKeysShowingViewModel(key);
        }
    }
}
