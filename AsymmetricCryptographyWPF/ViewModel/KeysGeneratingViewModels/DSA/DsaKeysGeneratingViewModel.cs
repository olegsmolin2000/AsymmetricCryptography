using AsymmetricCryptography;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA
{
    internal sealed class DsaKeysGeneratingViewModel : KeysGeneratingViewModel
    {
        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
              {
                  if (TryReadProperties())
                  {
                      DsaKeysGenerator keysGenerator = new DsaKeysGenerator(generationParameters);

                      DsaDomainParameter domainParameter;

                      int l = BinarySize;
                      int n = generationParameters.hashAlgorithm.GetDigestBitLength();

                      if (l <= n)
                          MessageBox.Show("Размер ключа должен быть больше длины хеша!");

                      domainParameter = keysGenerator.DsaDomainParametersGeneration(Name, l, n);

                      DataWorker.AddKey(domainParameter);

                      domainParameter = DataWorker.GetLastDomainParameter() as DsaDomainParameter;

                      keysGenerator.DsaKeysGeneration(Name, domainParameter, out privateKey, out publicKey);

                      FillDBAndClose(obj as Window);
                  }
              });
        }
    }
}
