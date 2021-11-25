using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels.DSA;
using System.Collections.Generic;
using System.Windows;

namespace AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA
{
    internal sealed class DsaKeysGenerationByDPViewModel : KeysGeneratingViewModel
    {
        private List<DsaDomainParameter> dsaDomainParameters;

        public List<DsaDomainParameter> DsaDomainParameters
        {
            get => dsaDomainParameters;

            set
            {
                dsaDomainParameters = value;

                NotifyPropertyChanged("DsaDomainParameters");
            }
        }

        public RelayCommand LoadParameters
        {
            get => new RelayCommand(obj =>
              {
                  DsaDomainParameters = DataWorker.GetDsaDomainParameters();
              });
        }



        private DsaDomainParameter domainParameter;
        public DsaDomainParameter DomainParameter
        {
            get => domainParameter;

            set
            {
                domainParameter = value;

                NotifyPropertyChanged("DomainParameter");

                SelectedDPViewModel = new DsaDomainParametersShowingViewModel(domainParameter);

                SelectedNumberGenerator = domainParameter.NumberGenerator;
                SelectedPrimalityVerificator = domainParameter.PrimalityVerificator;
                SelectedHashAlgorithm = domainParameter.HashAlgorithm;

                BinarySize = domainParameter.BinarySize;
            }
        }

        private DsaDomainParametersShowingViewModel selectedDPViewModel;
        public DsaDomainParametersShowingViewModel SelectedDPViewModel
        {
            get => selectedDPViewModel;
            set
            {
                selectedDPViewModel = value;

                NotifyPropertyChanged("SelectedDPViewModel");
            }
        }

        public RelayCommand OpenDPShowingWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window window = new DsaDomainParametersShowingWindow(DomainParameter);

                  window.Show();
              });
        }

        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
              {
                  if (TryReadProperties())
                  {
                      if (domainParameter == null)
                          MessageBox.Show("Нужно выбрать доменные параметры!");
                      else
                      {
                          DsaKeysGenerator keysGenerator = new DsaKeysGenerator(generationParameters);

                          keysGenerator.DsaKeysGeneration(Name, DomainParameter, out privateKey, out publicKey);

                          FillDBAndClose(obj as Window);
                      }
                  }
              });
        }
    }
}
