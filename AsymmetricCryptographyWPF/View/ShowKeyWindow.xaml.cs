using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel;
using System.Windows;

namespace AsymmetricCryptographyWPF.View
{
    /// <summary>
    /// Логика взаимодействия для ShowKeyWindow.xaml
    /// </summary>
    public partial class ShowKeyWindow : Window
    {
        public ShowKeyWindow(AsymmetricKey key)
        {

            
            InitializeComponent();
            this.DataContext = new ShowKeyViewModel(key);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
