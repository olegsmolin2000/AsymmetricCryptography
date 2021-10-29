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
    }
}
