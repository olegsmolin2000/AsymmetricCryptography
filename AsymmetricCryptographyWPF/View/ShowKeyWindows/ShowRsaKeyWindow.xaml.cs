using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel.ShowKeyViewModels;
using System.Windows;

namespace AsymmetricCryptographyWPF.View.ShowKeyWindows
{
    /// <summary>
    /// Логика взаимодействия для ShowRsaKeyWindow.xaml
    /// </summary>
    public partial class ShowRsaKeyWindow : Window
    {
        public ShowRsaKeyWindow(AsymmetricKey key)
        {
            InitializeComponent();

            DataContext = new ShowRsaKeyViewModel(key);
        }
    }
}
