using AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeysGeneratingWindows.DSA
{
    /// <summary>
    /// Логика взаимодействия для DsaKeysGeneratingByDPWindow.xaml
    /// </summary>
    public partial class DsaKeysGeneratingByDPWindow : Window
    {
        public DsaKeysGeneratingByDPWindow()
        {
            InitializeComponent();

            DataContext = new DsaKeysGenerationByDPViewModel();
        }
    }
}
