using AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeysGeneratingWindows
{
    /// <summary>
    /// Логика взаимодействия для ElGamalKeysGeneratingWindow.xaml
    /// </summary>
    public partial class ElGamalKeysGeneratingWindow : Window
    {
        public ElGamalKeysGeneratingWindow()
        {
            InitializeComponent();

            DataContext = new ElGamalKeysGeneratingViewModel();  
        }
    }
}
