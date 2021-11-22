using AsymmetricCryptographyWPF.ViewModel.GenerateKeysViewModels;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.GenerationKeysWindows
{
    /// <summary>
    /// Логика взаимодействия для RsaKeysGeneratingWindow.xaml
    /// </summary>
    public partial class RsaKeysGeneratingWindow : Window
    {
        public RsaKeysGeneratingWindow()
        {
            

            InitializeComponent();

            DataContext = new GenerateRsaKeysViewModel();
        }
    }
}
