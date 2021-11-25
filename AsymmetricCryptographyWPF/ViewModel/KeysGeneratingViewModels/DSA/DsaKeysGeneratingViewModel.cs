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

                      string[] paramsInfo = generationParameters.GetParameters();

                      domainParameter.NumberGenerator = paramsInfo[0];
                      domainParameter.PrimalityVerificator = paramsInfo[1];
                      domainParameter.HashAlgorithm = paramsInfo[2];

                      DataWorker.AddKey(domainParameter);

                      domainParameter = DataWorker.GetLastDomainParameter() as DsaDomainParameter;

                      keysGenerator.DsaKeysGeneration(Name, domainParameter, out privateKey, out publicKey);

                      FillDBAndClose(obj as Window);
                  }
              });
        }
    }
}
