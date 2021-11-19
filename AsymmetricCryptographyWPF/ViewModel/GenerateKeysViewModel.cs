using AsymmetricCryptography;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using AsymmetricCryptography.RSA;
using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AsymmetricCryptographyWPF.ViewModel
{
    class GenerateKeysViewModel:INotifyPropertyChanged
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;

                NotifyPropertyChanged("Name");
            }
        }

        TextBlock rng;
        public TextBlock RNG
        {
            get
            {
                return rng;
            }
            set
            {
                rng = value;

                NotifyPropertyChanged("RNG");
            }
        }

        //public string NumberGenerator { get; set; }
        public TextBlock PrimalityVerificator { get; set; }
        public TextBlock HashAlgorithm { get; set; }
        public TextBlock AlgorithmName { get; set; }
        public int BinarySize { get; set; }



        private RelayCommand generateKeys;

        public RelayCommand GenerateKeys
        {
            get
            {
                return generateKeys ?? new RelayCommand(obj =>
                {
                    string rngName = rng.Text;
                    string primalityVerificatorName = PrimalityVerificator.Text;
                    string hashAlgName = HashAlgorithm.Text;
                    string algorithmName = AlgorithmName.Text;

                    AsymmetricKey privateKey, publicKey;

                    PrimalityVerificator primality = new MillerRabinPrimalityVerificator();

                    NumberGenerator numberGenerator = new FibonacciNumberGenerator(primality);

                    //primality.SetNumberGenerator(numberGenerator);

                    CryptographicHashAlgorithm hashAlgorithm = new SHA_256();

                    Parameters parameters = new Parameters(numberGenerator, primality, hashAlgorithm);

                    KeysGenerator keysGenerator;

                    switch (algorithmName)
                    {
                        case "RSA":
                            {
                                keysGenerator = new RsaKeysGenerator(parameters);
                                break;
                            }
                        case "DSA":
                            {
                                keysGenerator = new DsaKeysGenerator(parameters);
                                break;
                            }
                        //case "ElGamal":
                        default:
                            {
                                keysGenerator = new ElGamalKeysGenerator(parameters);
                                break;
                            }
                    }

                    string[] paramsInfo= parameters.GetParameters();

                    keysGenerator.GenerateKeyPair(name, BinarySize, out privateKey, out publicKey);

                    privateKey.NumberGenerator = paramsInfo[0];
                    publicKey.NumberGenerator = paramsInfo[0];

                    privateKey.PrimalityVerificator = paramsInfo[1];
                    publicKey.PrimalityVerificator = paramsInfo[1];

                    privateKey.HashAlgorithm = paramsInfo[2];
                    publicKey.HashAlgorithm = paramsInfo[2];

                    DataWorker.AddKey(privateKey);
                    DataWorker.AddKey(publicKey);
                }
                );
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
