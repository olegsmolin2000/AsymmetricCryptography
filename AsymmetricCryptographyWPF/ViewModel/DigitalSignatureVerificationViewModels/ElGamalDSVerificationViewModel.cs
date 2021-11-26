using AsymmetricCryptography;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using System.Numerics;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.DigitalSignatureVerificationViewModels
{
    internal sealed class ElGamalDSVerificationViewModel:ViewModel
    {
        private AsymmetricKey Key { get; set; }

        private string r;
        public string R
        {
            get => r;
            set
            {
                r = value;

                NotifyPropertyChanged("R");
            }
        }

        private string s;
        public string S
        {
            get => s;
            set
            {
                s = value;

                NotifyPropertyChanged("S");
            }
        }

        private byte[] Data { get; set; }

        public ElGamalDSVerificationViewModel(AsymmetricKey key, byte[] data)
        {
            Key = key;

            Data = data;
        }

        public RelayCommand VerifySign
        {
            get => new RelayCommand(obj =>
            {
                BigInteger r = new BigInteger();
                BigInteger s = new BigInteger();

                if (R == "" || !BigInteger.TryParse(R, out r)
                || S == "" || !BigInteger.TryParse(S, out s))
                    MessageBox.Show("Значение подписи должно содержать только цифры!");
                else
                {
                    GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(Key.GetParametersInfo());

                    IDigitalSignatutator signatutator = null;

                    ElGamalDigitalSignature signature = new ElGamalDigitalSignature(r, s);

                    if (Key is ElGamalPublicKey)
                        signatutator = new ElGamalAlgorithm(parameters);
                    if (Key is DsaPublicKey)
                        signatutator = new DSA(parameters, (Key as DsaPublicKey).DomainParameter);

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
