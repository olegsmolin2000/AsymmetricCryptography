using AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.KeysGeneratingWindows.DSA
{
    /// <summary>
    /// Логика взаимодействия для DsaDomainParameterGeneratingWindow.xaml
    /// </summary>
    public partial class DsaDomainParameterGeneratingWindow : Window
    {
        public DsaDomainParameterGeneratingWindow()
        {
            InitializeComponent();

            DataContext = new DsaDomainParametersGeneratingViewModel();
        }
    }
}
