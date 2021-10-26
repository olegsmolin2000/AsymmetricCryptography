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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AsymmetricCryptography;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using AsymmetricCryptography.RSA;

namespace AsymmetricCryptographyWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AsymmetricKey privateKey, publicKey;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateKeysButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateKeysWindow wind = new GenerateKeysWindow();
            wind.Owner = Application.Current.MainWindow;
            wind.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wind.ShowDialog();
            //wind.Closed += new EventHandler(PrintKeys);
        }

        public void SetCurrentKeys(AsymmetricKey privateKey,AsymmetricKey publicKey)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;

            PrintKeys();
        }

        public void PrintKeys()
        {
            if (privateKey != null)
                PrivateKeyTextBox.Text = privateKey.ToString();
            if (publicKey != null)
                PublicKeyTextBox.Text = publicKey.ToString();
        }
    }
}
