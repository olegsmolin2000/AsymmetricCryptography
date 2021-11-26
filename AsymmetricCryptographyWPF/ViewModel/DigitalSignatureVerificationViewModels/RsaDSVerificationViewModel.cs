using AsymmetricCryptography;
using AsymmetricCryptography.RSA;
using AsymmetricCryptographyDAL.Entities.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.DigitalSignatureVerificationViewModels
{
    internal sealed class RsaDSVerificationViewModel:ViewModel
    {
        private AsymmetricKey Key { get; set; }

        private string signValue;

        public string SignValue
        {
            get => signValue;
            set
            {
                signValue = value;

                NotifyPropertyChanged("SignValue");
            }
        }

        private byte[] Data { get; set; }

        public RsaDSVerificationViewModel(AsymmetricKey key, byte[] data)
        {
            Key = key;

            Data = data;
        }

        public RelayCommand VerifySign
        {
            get => new RelayCommand(obj =>
              {
                  BigInteger sign = new BigInteger();

                  if (SignValue == "" || !BigInteger.TryParse(SignValue, out sign))
                      MessageBox.Show("Значение ключа должно содержать только цифры!");
                  else
                  {
                      GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(Key.GetParametersInfo());

                      IDigitalSignatutator signatutator = new RsaAlgorithm(parameters);

                      RsaDigitalSignature signature = new RsaDigitalSignature(BigInteger.Parse(SignValue));

                      bool isCorrect = signatutator.VerifyDigitalSignature(signature, Data, Key);

                      if (isCorrect)
                          MessageBox.Show("Подпись верна!");
                      else
                          MessageBox.Show("Подпись неверна!");
                  }
              });
        }
    }
}
