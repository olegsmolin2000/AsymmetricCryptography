using AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeysGeneratingWindows.DSA
{
    /// <summary>
    /// Логика взаимодействия для DsaKeysGeneratingWindow.xaml
    /// </summary>
    public partial class DsaKeysGeneratingWindow : Window
    {
        public DsaKeysGeneratingWindow()
        {
            InitializeComponent();

            DataContext = new DsaKeysGeneratingViewModel();
        }
    }
}
