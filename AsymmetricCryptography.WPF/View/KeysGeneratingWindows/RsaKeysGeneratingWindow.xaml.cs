using AsymmetricCryptography.WPF.ViewModel.KeysGenerating;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeysGeneratingWindows
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
