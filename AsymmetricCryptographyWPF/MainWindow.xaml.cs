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

namespace AsymmetricCryptographyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateKeysButton_Click(object sender, RoutedEventArgs e)
        {
            PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();
            NumberGenerator numberGenerator = new FibonacciNumberGenerator(primalityVerificator);
            primalityVerificator.SetNumberGenerator(numberGenerator);

            CryptographicHashAlgorithm hashAlgorithm = new SHA_256();

            Parameters parameters = new Parameters(numberGenerator, primalityVerificator, hashAlgorithm);

            AsymmetricKey privateKey, publicKey;
            KeysGenerator generator;

            int binarySize = Convert.ToInt32(BinarySizeTextBox.Text);

            string algorithmName = (AlgNameComboBox.SelectedValue as TextBlock).Text;

            switch (algorithmName)
            {
                case "RSA":
                    generator = new RsaKeysGenerator(parameters);
                    break;
                case "DSA":
                    generator = new DsaKeysGenerator(parameters);
                    break;
                default:
                    generator = new ElGamalKeysGenerator(parameters);
                    break;
            }

            generator.GenerateKeyPair(binarySize, out privateKey, out publicKey);

            PrivateKeyTextBox.Text = privateKey.ToString();
            PublicKeyTextBox.Text = publicKey.ToString();
        }
    }
}
