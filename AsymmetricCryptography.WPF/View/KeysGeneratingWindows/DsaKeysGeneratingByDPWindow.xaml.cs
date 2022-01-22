using AsymmetricCryptography.WPF.ViewModel.KeysGenerating;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeysGeneratingWindows
{
    /// <summary>
    /// Логика взаимодействия для DsaKeysGeneratingByDPWindow.xaml
    /// </summary>
    public partial class DsaKeysGeneratingByDPWindow : Window
    {
        public DsaKeysGeneratingByDPWindow()
        {
            InitializeComponent();

            DataContext = new DsaKeysGeneratingByDPViewModel();
        }
    }
}
