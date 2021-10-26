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
using AsymmetricCryptography;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;
using AsymmetricCryptography.CryptographicHash;


namespace AsymmetricCryptographyWPF.View
{
    /// <summary>
    /// Логика взаимодействия для GenerateKeysWindow.xaml
    /// </summary>
    public partial class GenerateKeysWindow : Window
    {
        public GenerateKeysWindow()
        {
            InitializeComponent();
        }

        private void GenerateKeysButton_Click(object sender,RoutedEventArgs e)
        {
            PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();
            NumberGenerator numberGenerator = new FibonacciNumberGenerator(primalityVerificator);
            primalityVerificator.SetNumberGenerator(numberGenerator);
            CryptographicHashAlgorithm hashAlgorithm = new SHA_256();

            Parameters parameters = new Parameters(numberGenerator, primalityVerificator, hashAlgorithm);

            KeysGenerator generator;

            switch (AlgNameComboBox.Text)
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

            AsymmetricKey privateKey, publicKey;

            int binarySize = Convert.ToInt32(BinarySizeTextBox.Text);

            generator.GenerateKeyPair(binarySize, out privateKey, out publicKey);

            (this.Owner as MainWindow).SetCurrentKeys(privateKey,publicKey);

            this.Close();
        }
    }
}
