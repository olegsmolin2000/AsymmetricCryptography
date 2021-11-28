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
using AsymmetricCryptographyWPF.View.DigitalSignatureVerificationWindows;
using AsymmetricCryptographyWPF.ViewModel.DigitalSignatureVerificationViewModels;
using AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors;
using Microsoft.Win32;
using System.IO;
using System.Text;

namespace AsymmetricCryptographyWPF.ViewModel
{
    class MainWindowViewModel:ViewModel
    {
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

        public RelayCommand LoadKeys
        {
            get => new RelayCommand(obj =>
            {
                AllKeys = DataWorker.GetAll();
            });
        }


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

                SelectedKeyShowingVM = new KeysShowingViewModel(SelectedKey); 
            }
        }
        #endregion

        #region TextFields
        private string inputedText;
        public string InputedText
        {
            get => inputedText;

            set
            {
                inputedText = value;

                NotifyPropertyChanged("InputedText");
            }
        }

        byte[] initialBytes;

        byte[] encryptedBytes;
        byte[] decryptedBytes;

        byte[] openedFileBytes;

        private string resultText;
        public string ResultText
        {
            get => resultText;
            set
            {
                resultText = value;

                NotifyPropertyChanged("ResultText");
            }
        }
        #endregion

        #region EncryptionCommands
        private byte[] StringToByte(string str)
        {
            //byte[] bytes = new byte[str.Length];

            //for (int i = 0; i < str.Length; i++)
            //{
            //    char currentSymb = str[i];
            //    //ASCIIEncoding
            //    int s = str[i];

            //    var encoding = Encoding.ASCII;
                
            //    bytes[i] = Convert.ToByte(currentSymb);
            //}

            //return bytes;

            return Encoding.Unicode.GetBytes(str);
        }

        private string BytesToString(byte[] arr)
        {
            //string str = "";

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    str += Convert.ToChar(arr[i]);
            //}

            //return str;

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
                    else if (InputedText == null || InputedText == "")
                        MessageBox.Show("Введите текст!");
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

                        byte[] textBytes;

                        if (openedFileBytes != null)
                            textBytes = openedFileBytes;
                        else
                            textBytes = StringToByte(InputedText);

                        initialBytes = StringToByte(InputedText);

                        encryptedBytes = encryptor.Encrypt(textBytes, selectedKey);

                        ResultText = BytesToString(encryptedBytes);

                        openedFileBytes = null;
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
                    else if (inputedText == null || inputedText == "")
                        MessageBox.Show("Введите текст!");
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

                        byte[] textBytes;

                        if (openedFileBytes != null)
                            textBytes = openedFileBytes;
                        else
                            textBytes = StringToByte(inputedText);

                        decryptedBytes = encryptor.Decrypt(encryptedBytes, selectedKey);

                        ResultText = BytesToString(decryptedBytes);

                        openedFileBytes = null;
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
                  if (IsValidKey("Private") && inputedText == null && inputedText == "")
                  {
                      GeneratingParameters parameters = GeneratingParameters.GetParametersByInfo(selectedKey.GetParametersInfo());

                      IDigitalSignatutator signatutor = null;

                      if (selectedKey.AlgorithmName == "RSA")
                          signatutor = new RsaAlgorithm(parameters);
                      else if (selectedKey.AlgorithmName == "ElGamal")
                          signatutor = new ElGamalAlgorithm(parameters);
                      else if (selectedKey.AlgorithmName == "DSA")
                          signatutor = new DSA(parameters, (selectedKey as DsaPrivateKey).DomainParameter);
                      else
                          MessageBox.Show("Ключ такого типа не поддерживается!");

                      var inputedTextBytes = StringToByte(inputedText);

                      var sign = signatutor.CreateSignature(inputedTextBytes, selectedKey);

                      ResultText = sign.ToString();
                  }
                  else
                      MessageBox.Show("Введите текст!");
              });
        }

        public RelayCommand VerificationDigitalSignatur
        {
            get => new RelayCommand(obj =>
            {
                if (IsValidKey("Public") && inputedText == null && inputedText == "")
                {
                    var inputedTextBytes = StringToByte(inputedText);

                    Window window = null;

                    if (selectedKey.AlgorithmName == "RSA")
                    {
                        window = new RsaDSVerificationWindow();

                        window.DataContext = new RsaDSVerificationViewModel(selectedKey, inputedTextBytes);
                    }
                    else if (selectedKey.AlgorithmName == "ElGamal" || selectedKey.AlgorithmName == "DSA")
                    {
                        window = new ElGamalDSVerificationWindow();

                        window.DataContext = new ElGamalDSVerificationViewModel(selectedKey, inputedTextBytes);
                    }
                   
                    window.Show();
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

        #region KeysXmlSaving
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
        #endregion

        #region OpenAndSaveTxt
        public RelayCommand OpenInputTextFile
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
                          InputedText = "";

                          var filePath = openFileDialog.FileName;

                          using (StreamReader streamReader = new StreamReader(filePath))
                          {
                              InputedText = streamReader.ReadToEnd();
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.GetType().ToString());
                      }
                  }
              });
        }

        public RelayCommand SaveOutputTxtFile
        {
            get => new RelayCommand(obj =>
            {
                SaveFileDialog openFileDialog = new SaveFileDialog()
                {
                    Filter = "Text files (*.txt)|*.txt",
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        //using(StreamWriter streamWriter=new StreamWriter()
                        var filePath = openFileDialog.FileName;

                        File.WriteAllBytes(filePath, decryptedBytes);
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
