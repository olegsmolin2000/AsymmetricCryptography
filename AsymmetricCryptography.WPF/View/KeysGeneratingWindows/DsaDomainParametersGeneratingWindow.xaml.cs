using AsymmetricCryptography.WPF.ViewModel.KeysGenerating;
using System.Windows;

namespace AsymmetricCryptography.WPF.View.KeysGeneratingWindows
{
    /// <summary>
    /// Логика взаимодействия для DsaDomainParametersGeneratingWindow.xaml
    /// </summary>
    public partial class DsaDomainParametersGeneratingWindow : Window
    {
        public DsaDomainParametersGeneratingWindow()
        {
            InitializeComponent();

            DataContext = new DsaDomainParametersGeneratingViewModel();
        }
    }
}
