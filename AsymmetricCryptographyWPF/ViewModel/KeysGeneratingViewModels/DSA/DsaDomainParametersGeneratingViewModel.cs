using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA
{
    internal sealed class DsaDomainParametersGeneratingViewModel : KeysGeneratingViewModel
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

                      CloseWindow(obj as Window);
                  }
              });
        }
    }
}
