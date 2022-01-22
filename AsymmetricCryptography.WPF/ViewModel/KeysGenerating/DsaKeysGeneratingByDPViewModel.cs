using AsymmetricCryptography.Core.KeysGenerators;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.EFCore.Repositories;
using AsymmetricCryptography.WPF.View.KeyShowingWindows;
using AsymmetricCryptography.WPF.ViewModel.KeyShowing;
using System.Collections.Generic;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel.KeysGenerating
{
    internal sealed class DsaKeysGeneratingByDPViewModel:KeysGeneratingViewModel
    {
        private List<DsaDomainParameter> dsaDomainParameters;
        public List<DsaDomainParameter> DsaDomainParameters
        {
            get => dsaDomainParameters;

            set
            {
                dsaDomainParameters = value;

                NotifyPropertyChanged(nameof(DsaDomainParameters));
            }
        }

        private DsaDomainParameterShowingViewModel selectedDPViewModel;
        public DsaDomainParameterShowingViewModel SelectedDPViewModel
        {
            get => selectedDPViewModel;
            set
            {
                selectedDPViewModel = value;

                NotifyPropertyChanged(nameof(SelectedDPViewModel));
            }
        }

        private DsaDomainParameter selectedDomainParameter;
        public DsaDomainParameter SelectedDomainParameter
        {
            get => selectedDomainParameter;

            set
            {
                selectedDomainParameter = value;

                NotifyPropertyChanged(nameof(SelectedDomainParameter));

                SelectedDPViewModel = new DsaDomainParameterShowingViewModel(selectedDomainParameter);

                SelectedNumberGenerator = selectedDomainParameter.NumberGenerator.ToString();
                SelectedPrimalityTest = selectedDomainParameter.PrimalityVerificator.ToString();
                SelectedHashAlgorithm = selectedDomainParameter.HashAlgorithm.ToString();

                BinarySize = selectedDomainParameter.BinarySize;
            }
        }

        public DsaKeysGeneratingByDPViewModel()
        {
            KeysRepository<DsaDomainParameter> repository = new KeysRepository<DsaDomainParameter>();

            DsaDomainParameters = repository.Items;
        }

        public RelayCommand OpenDPShowingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window window = new DsaDomainParameterShowingWindow(SelectedDomainParameter);

                window.Show();
            });
        }

        public override RelayCommand GenerateKeys
        {
            get => new RelayCommand(obj =>
            {
                if (TryReadProperties())
                {
                    if (SelectedDomainParameter == null)
                        MessageBox.Show("Нужно выбрать доменные параметры!");
                    else
                    {
                        DsaKeysGenerator keysGenerator = new DsaKeysGenerator(selectedNumberGenerator, selectedPrimalityTest, selectedHashAlgorithm);

                        keysGenerator.DsaKeysGeneration(SelectedDomainParameter, out privateKey, out publicKey);

                        SetParameters(privateKey);
                        SetParameters(publicKey);

                        Repository.Add(privateKey);
                        Repository.Add(publicKey);

                        CloseWindow(obj as Window);
                    }
                }
            });
        }
    }
}
