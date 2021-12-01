using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys;
using System.Collections.Generic;
using System.Windows;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using AsymmetricCryptographyWPF.View.KeysGeneratingWindows;
using AsymmetricCryptographyWPF.View.KeyShowingWindows;
using AsymmetricCryptographyWPF.View.KeysGeneratingWindows.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyWPF.View.KeyShowingWindows.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using AsymmetricCryptography;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.ElGamal;
using System;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace AsymmetricCryptographyWPF.ViewModel
{
    class MainWindowViewModel:ViewModel
    {
        #region KeysList
        private List<AsymmetricKey> allKeys;

        public List<AsymmetricKey> AllKeys
        {
            get => allKeys;

            set
            {
                allKeys = value;
                NotifyPropertyChanged("AllKeys");
            }
        }

        public void RefreshData()
        {
            LoadKeys.Execute(null);
        }

        public RelayCommand DeleteAll
        {
            get => new RelayCommand(obj =>
              {
                  SelectedKey = null;

                  DataWorker.ClearDB();

                  RefreshData();

                  MessageBox.Show("База данных успешно очищенна!");
              });
        }

        public RelayCommand LoadKeys
        {
            get => new RelayCommand(obj =>
            {
                AllKeys = DataWorker.GetAll();
            });
        }
        #endregion

        #region SelectedKey
        private KeysShowingViewModel selectedKeyShowingVM;
        public KeysShowingViewModel SelectedKeyShowingVM
        {
            get => selectedKeyShowingVM;

            set
            {
                selectedKeyShowingVM = value;

                NotifyPropertyChanged("SelectedKeyShowingVM");
            }
        }


        private AsymmetricKey selectedKey;
        public AsymmetricKey SelectedKey
        {
            get => selectedKey;

            set
            {
                selectedKey = value;

                NotifyPropertyChanged("SelectedKey");

                if (selectedKey != null)
                    SelectedKeyShowingVM = new KeysShowingViewModel(SelectedKey);
                else
                    SelectedKeyShowingVM = null;
            }
        }
        #endregion

        #region TextFields
        private string mainTextbox;
        public string MainTextBox
        {
            get => mainTextbox;

            set
            {
                mainTextbox = value;

                NotifyPropertyChanged("MainTextBox");
            }
        }
        #endregion

        #region Encryptions
        private byte[] StringToByte(string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }

        private string BytesToString(byte[] arr)
        {
            return Encoding.Unicode.GetString(arr);
        }

        private bool IsValidKey(string type)
        {
            if (selectedKey == null)
            {
                MessageBox.Show("Выберите ключ!");

                return false;
            }
            else if (selectedKey.Type != type)
            {
                MessageBox.Show("Выберите " + type + " ключ!");

                return false;
            }

            return true;
        }

        public RelayCommand Encrypt
        {
            get => new RelayCommand(obj =>
            {
                if (IsValidKey("Public"))
                {
                    if (selectedKey.AlgorithmName == "DSA")
                        MessageBox.Show("Алгоритм DSA не поддерживает шифрование!");
                    else if (MainTextBox == null || MainTextBox == "")
                        MessageBox.Show("Введите текст!");
                    else
                    {
                        string[] keyParametersInfo = selectedKey.GetParametersInfo();

                        keyParametersInfo[0] = "Lagged Fibonacci";

                        GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(keyParametersInfo);

                        IEncryptor encryptor = null;

                        if (selectedKey.AlgorithmName == "RSA")
                            encryptor = new RsaAlgorithm(parameters);
                        else if (selectedKey.AlgorithmName == "ElGamal")
                            encryptor = new ElGamalAlgorithm(parameters);
                        else
                            MessageBox.Show("Ключ такого типа не поддерживается!");

                        if (MainTextBox != null && MainTextBox != "")
                        {
                            byte[] bytesToEncrypt = StringToByte(MainTextBox);

                            byte[] encryptedBytes = encryptor.Encrypt(bytesToEncrypt, selectedKey);

                            SaveFileDialog saveFileDialog = new SaveFileDialog();

                            if (saveFileDialog.ShowDialog() == true)
                            {
                                try
                                {
                                    var filePath = saveFileDialog.FileName;

                                    File.WriteAllBytes(filePath, encryptedBytes);

                                    MessageBox.Show("Файл с шифровкой успешно сохранён!");

                                    MainTextBox = "";
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.GetType().ToString());
                                }
                            }
                        }
                        else
                            MessageBox.Show("Введите текст!");
                    }
                }
            });
        }

        public RelayCommand Decrypt
        {
            get => new RelayCommand(obj =>
            {
                if (IsValidKey("Private"))
                {
                    if (selectedKey.AlgorithmName == "DSA")
                        MessageBox.Show("Алгоритм DSA не поддерживает шифрование!");
                    else
                    {
                        GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(selectedKey.GetParametersInfo());

                        IEncryptor encryptor = null;

                        if (selectedKey.AlgorithmName == "RSA")
                            encryptor = new RsaAlgorithm(parameters);
                        else if (selectedKey.AlgorithmName == "ElGamal")
                            encryptor = new ElGamalAlgorithm(parameters);
                        else
                            MessageBox.Show("Ключ такого типа не поддерживается!");

                        OpenFileDialog openFileDialog = new OpenFileDialog();

                        byte[] bytesToDecrypt;

                        if (openFileDialog.ShowDialog() == true)
                        {
                            try
                            {
                                var filePath = openFileDialog.FileName;

                                bytesToDecrypt = File.ReadAllBytes(filePath);

                                byte[] decryptedBytes = encryptor.Decrypt(bytesToDecrypt, selectedKey);

                                MainTextBox = BytesToString(decryptedBytes);

                                MessageBox.Show("Файл успешно расшифрован!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.GetType().ToString());
                            }
                        }
                    }
                }
            });
        }
        #endregion

        #region DigitalSignature
        public RelayCommand CreatingDigitalSignatur
        {
            get => new RelayCommand(obj =>
              {
                  if (IsValidKey("Private") && MainTextBox != null && MainTextBox != "")
                  {
                      GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(selectedKey.GetParametersInfo());

                      IDigitalSignatutator signatutor = null;

                      if (selectedKey.AlgorithmName == "RSA")
                          signatutor = new RsaAlgorithm(parameters);
                      else if (selectedKey.AlgorithmName == "ElGamal")
                          signatutor = new ElGamalAlgorithm(parameters);
                      else if (selectedKey.AlgorithmName == "DSA")
                      {
                          int domainParameterId = (int)(selectedKey as DsaPrivateKey).DomainParameterId;

                          DsaDomainParameter domainParameter = DataWorker.GetKey(domainParameterId) as DsaDomainParameter;

                          signatutor = new DSA(parameters, domainParameter);
                      }
                      else
                          MessageBox.Show("Ключ такого типа не поддерживается!");

                      var bytesToCreateSignature = StringToByte(MainTextBox);

                      var sign = signatutor.CreateSignature(bytesToCreateSignature, selectedKey);

                      SaveFileDialog saveFileDialog = new SaveFileDialog();

                      saveFileDialog.Filter = "XML-File | *.xml";

                      if (saveFileDialog.ShowDialog() == true)
                      {
                          string filePath = saveFileDialog.FileName;

                          sign.WriteXml(filePath);

                          MessageBox.Show("Подпись успешно записана в файл!");
                      }
                  }
                  else
                      MessageBox.Show("Введите текст!");
              });
        }

        public RelayCommand VerificationDigitalSignatur
        {
            get => new RelayCommand(obj =>
            {
                if (IsValidKey("Public") && MainTextBox != null && MainTextBox != "")
                {
                    byte[] bytesToVerificate = StringToByte(MainTextBox);

                    OpenFileDialog openFileDialog = new OpenFileDialog();

                    openFileDialog.Filter = "XML-File | *.xml";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        IDigitalSignatutator signatutator = null;

                        DigitalSignature signature = null;

                        GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(selectedKey.GetParametersInfo());

                        if (selectedKey.AlgorithmName == "RSA")
                        {
                            signatutator = new RsaAlgorithm(parameters);

                            signature = new RsaDigitalSignature(openFileDialog.FileName);


                        }
                        else
                        {
                            signature = new ElGamalDigitalSignature(openFileDialog.FileName);

                            if (selectedKey.AlgorithmName == "DSA")
                            {
                                int domainParameterId = (int)(selectedKey as DsaPublicKey).DomainParameterId;

                                DsaDomainParameter domainParameter = DataWorker.GetKey(domainParameterId) as DsaDomainParameter;

                                signatutator = new DSA(parameters, domainParameter);
                            }
                            else if (selectedKey.AlgorithmName == "ElGamal")
                                signatutator = new ElGamalAlgorithm(parameters);
                        }

                        bool isCorrect = signatutator.VerifyDigitalSignature(signature, bytesToVerificate, selectedKey);

                        if (isCorrect)
                            MessageBox.Show("Подпись верна!");
                        else
                            MessageBox.Show("Подпись неверна!");
                    }

                    //Window window = null;

                    //if (selectedKey.AlgorithmName == "RSA")
                    //{
                    //    //TODO: Open XML signature

                    //    window = new RsaDSVerificationWindow();

                    //    window.DataContext = new RsaDSVerificationViewModel(selectedKey, inputedTextBytes);
                    //}
                    //else if (selectedKey.AlgorithmName == "ElGamal" || selectedKey.AlgorithmName == "DSA")
                    //{
                    //    //TODO: Open XML signature

                    //    window = new ElGamalDSVerificationWindow();

                    //    window.DataContext = new ElGamalDSVerificationViewModel(selectedKey, inputedTextBytes);
                    //}

                    //window.Show();
                }
                else
                    MessageBox.Show("Введите текст!");
            });
        }
        #endregion

        #region OpenWindows
        public RelayCommand OpenShowKeyWindow
        {
            get => new RelayCommand(obj =>
            {
                Window window;

                if (SelectedKey is RsaPrivateKey || SelectedKey is RsaPublicKey)
                {
                    window = new RsaKeyShowingWindow(SelectedKey);
                    window.Show();
                }
                else if (selectedKey is DsaDomainParameter)
                {
                    window = new DsaDomainParametersShowingWindow(SelectedKey);
                    window.Show();
                }
                else if (selectedKey is DsaPrivateKey || selectedKey is DsaPublicKey)
                {
                    window = new DsaKeyShowingWindow(selectedKey);
                    window.Show();
                }
                else if (selectedKey is ElGamalPrivateKey || selectedKey is ElGamalPublicKey)
                {
                    window = new ElGamalKeyShowingWindow(selectedKey);
                    window.Show();
                }
            });
        }

        public RelayCommand OpenGenerateRsaKeysWindow
        {
            get => new RelayCommand(obj =>
            {
                Window rsaKeysGenerationWindow = new RsaKeysGeneratingWindow();

                rsaKeysGenerationWindow.Owner = Application.Current.MainWindow;

                rsaKeysGenerationWindow.Show();
            });
        }

        public RelayCommand OpenDsaDPGeneratingWindow
        {
            get => new RelayCommand(obj =>
             {
                 Window generatingWindow = new DsaDomainParameterGeneratingWindow();

                 generatingWindow.Owner = Application.Current.MainWindow;

                 generatingWindow.Show();
             });
        }

        public RelayCommand OpenDsaKeysGeneratingByDPWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window generatingWindow = new DsaKeysGeneratingByDPWindow();

                  generatingWindow.Owner = Application.Current.MainWindow;

                  generatingWindow.Show();
              });
        }

        public RelayCommand OpenDsaKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window generatingWindow = new DsaKeysGeneratingWindow();

                  generatingWindow.Owner = Application.Current.MainWindow;

                  generatingWindow.Show();
              });
        }

        public RelayCommand OpenElGamalKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
              {
                  Window generatingWindow = new ElGamalKeysGeneratingWindow();

                  generatingWindow.Owner = Application.Current.MainWindow;

                  generatingWindow.Show();
              });
        }
        #endregion

        #region KeysXml
        public RelayCommand XmlSaveSelectedKey
        {
            get => new RelayCommand(obj =>
              {
                  if (selectedKey != null)
                  {
                      XmlKeyVisitor keyVisitor = new XmlKeyVisitor();

                      selectedKey.Accept(keyVisitor);

                      SaveFileDialog saveFileDialog = new SaveFileDialog();

                      saveFileDialog.Filter = "XML-File | *.xml";

                      if (saveFileDialog.ShowDialog() == true)
                      {
                          keyVisitor.xDoc.Save(saveFileDialog.FileName);

                          MessageBox.Show("Ключ успешно сохранён!");
                      }
                  }
                  else
                      MessageBox.Show("Выберите ключ!");
              });
        }

        public RelayCommand OpenXmlKey
        {
            get => new RelayCommand(obj =>
              {
                  OpenFileDialog openFileDialog = new OpenFileDialog();

                  openFileDialog.Filter = "XML-File | *.xml";

                  if (openFileDialog.ShowDialog() == true)
                  {
                      string filePath = openFileDialog.FileName;

                      SelectedKey = AsymmetricKey.ReadXml(XElement.Load(filePath));

                      RefreshData();
                  }
              });
        }
        #endregion

        #region OpenAndSaveTxt
        public RelayCommand OpenTextFile
        {
            get => new RelayCommand(obj =>
              {
                  OpenFileDialog openFileDialog = new OpenFileDialog()
                  {
                      Filter = "Text files (*.txt)|*.txt",
                  };

                  if (openFileDialog.ShowDialog() == true)
                  {
                      try
                      {
                          var filePath = openFileDialog.FileName;

                          MainTextBox = File.ReadAllText(filePath);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.GetType().ToString());
                      }
                  }
              });
        }

        public RelayCommand SaveTextFile
        {
            get => new RelayCommand(obj =>
             {
                 SaveFileDialog saveFileDialog = new SaveFileDialog()
                 {
                     Filter = "Text files (*.txt)|*.txt",
                 };

                 if (saveFileDialog.ShowDialog() == true)
                 {
                     try
                     {
                         var filePath = saveFileDialog.FileName;

                         File.WriteAllText(filePath, MainTextBox);

                         MessageBox.Show("Файл успешно сохранён!");
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.GetType().ToString());
                     }
                 }
             });
        }
        #endregion
    }
}
