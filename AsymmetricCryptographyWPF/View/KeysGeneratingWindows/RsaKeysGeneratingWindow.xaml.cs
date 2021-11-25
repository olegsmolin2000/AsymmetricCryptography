using AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeysGeneratingWindows
{
    /// <summary>
    /// Логика взаимодействия для RsaKeysGeneratingWindow.xaml
    /// </summary>
    public partial class RsaKeysGeneratingWindow : Window
    {
        public RsaKeysGeneratingWindow()
        {
            InitializeComponent();

            DataContext = new RsaKeysGeneratingViewModel();
        }
    }
}
