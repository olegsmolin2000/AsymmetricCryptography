using AsymmetricCryptography.WPF.ViewModel.KeysGenerating;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeysGeneratingWindows
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
