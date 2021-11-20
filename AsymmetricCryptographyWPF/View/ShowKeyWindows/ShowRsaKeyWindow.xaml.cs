using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyWPF.ViewModel.ShowKeyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
