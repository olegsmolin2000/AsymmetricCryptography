using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA;
using AsymmetricCryptographyWPF.ViewModel.KeysShowingViewModels.DSA;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;

namespace AsymmetricCryptographyWPF.ViewModel.KeysGeneratingViewModels.DSA
{
    internal sealed class DsaKeysGeneratingByDPViewModel : KeysGeneratingViewModel
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

        public RelayCommand OpenXmlDsaDomainParameters
        {
            get => new RelayCommand(obj =>
              {
                  OpenFileDialog openFileDialog = new OpenFileDialog();

                  openFileDialog.Filter = "XML-File | *.xml";

                  if (openFileDialog.ShowDialog() == true)
                  {
                      string filePath = openFileDialog.FileName;

                      DsaDomainParameter loadedDomainParameter = AsymmetricKey.ReadXml(XElement.Load(filePath)) as DsaDomainParameter;

                      if (loadedDomainParameter == null)
                          MessageBox.Show("Нужно загрузить DSA Domain Parameter!");
                      else
                      {
                          DataWorker.AddKey(loadedDomainParameter);

                          LoadParameters.Execute(null);

                          DomainParameter = DataWorker.GetLastDomainParameter() as DsaDomainParameter;
                      }
                  }
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
                  else
                      MessageBox.Show("Выберите параметры!");
              });
        }
    }
}
