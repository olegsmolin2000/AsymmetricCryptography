using AsymmetricCryptography.Core;
using AsymmetricCryptography.DataUnits.DigitalSignatures;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using AsymmetricCryptography.DataUnits.Keys.RSA;
using AsymmetricCryptography.EFCore.Repositories;
using AsymmetricCryptography.IO;
using AsymmetricCryptography.WPF.View.KeysGeneratingWindows;
using AsymmetricCryptography.WPF.View.KeyShowingWindows;
using AsymmetricCryptography.WPF.ViewModel.KeyShowing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace AsymmetricCryptography.WPF.ViewModel
{
    internal class MainWindowViewModel:ViewModel
    {
        #region Keys
        private readonly KeysRepository<AsymmetricKey> _keysRepository;

        private List<AsymmetricKey> keys;
        public List<AsymmetricKey> Keys
        {
            get => keys;

            set
            {
                keys = value;

                NotifyPropertyChanged(nameof(Keys));
            }
        }

        public RelayCommand LoadKeys
        {
            get => new RelayCommand(obj =>
              {
                  Keys = _keysRepository.Items;
              });
        }

        private AsymmetricKey selectedKey;
        public AsymmetricKey SelectedKey
        {
            get => selectedKey;

            set
            {
                selectedKey = value;

                NotifyPropertyChanged(nameof(SelectedKey));

                if (selectedKey != null)
                    SelectedKeyShowingVM = new KeyShowingViewModel(SelectedKey);
                else
                    SelectedKeyShowingVM = null;
            }
        }

        private KeyShowingViewModel selectedKeyShowingVM;
        public KeyShowingViewModel SelectedKeyShowingVM
        {
            get => selectedKeyShowingVM;

            set
            {
                selectedKeyShowingVM = value;

                NotifyPropertyChanged(nameof(SelectedKeyShowingVM));
            }
        }

        #endregion

        private string mainTextbox;
        public string MainTextBox
        {
            get => mainTextbox;

            set
            {
                mainTextbox = value;

                NotifyPropertyChanged(nameof(MainTextBox));
            }
        }

        public MainWindowViewModel()
        {
            _keysRepository = new KeysRepository<AsymmetricKey>();
        }

        #region KeysXml
        public RelayCommand XmlSaveSelectedKey
        {
            get => new RelayCommand(obj =>
            {
                if (selectedKey != null)
                {
                    XmlKeyWriter keyWriter = new XmlKeyWriter();

                    selectedKey.Accept(keyWriter);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.Filter = "XML-File | *.xml";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        keyWriter.Save(saveFileDialog.FileName);

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

                    //SelectedKey = AsymmetricKey.ReadXml(XElement.Load(filePath));

                    //RefreshData();
                }
            });
        }
        #endregion

        #region OpenWindows
        public RelayCommand OpenKeyShowingWindow
        {
            get => new RelayCommand(obj =>
            {
                if (SelectedKey is null)
                    MessageBox.Show("Выберите ключ!");
                else
                {
                    Window keyShowingWnd;

                    if (SelectedKey is RsaPrivateKey || SelectedKey is RsaPublicKey)
                    {
                        keyShowingWnd = new RsaKeyShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                    else if (SelectedKey is ElGamalPrivateKey || SelectedKey is ElGamalPublicKey)
                    {
                        keyShowingWnd = new ElGamalKeyShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                    else if (SelectedKey is DsaDomainParameter)
                    {
                        keyShowingWnd = new DsaDomainParameterShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                    else if (SelectedKey is DsaPrivateKey || SelectedKey is DsaPublicKey)
                    {
                        keyShowingWnd = new DsaKeyShowingWindow(SelectedKey);

                        keyShowingWnd.Show();
                    }
                }
            });
        }

        public RelayCommand OpenRsaKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window rsaKeysGenerationWindow = new RsaKeysGeneratingWindow();

                rsaKeysGenerationWindow.Owner = Application.Current.MainWindow;

                rsaKeysGenerationWindow.Show();
            });
        }

        public RelayCommand OpenElGamalKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window elGamalKeysGenerationWindow = new ElGamalKeysGeneratingWindow();

                elGamalKeysGenerationWindow.Owner = Application.Current.MainWindow;

                elGamalKeysGenerationWindow.Show();
            });
        }

        public RelayCommand OpenDsaDomainParameterGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window dsaDPGeneratingWnd = new DsaDomainParametersGeneratingWindow();

                dsaDPGeneratingWnd.Owner = Application.Current.MainWindow;

                dsaDPGeneratingWnd.Show();
            });
        }

        public RelayCommand OpenDsaKeysGeneratingWindow
        {
            get => new RelayCommand(obj =>
            {
                Window dsaKeysGeneratingWnd = new DsaKeysGeneratingWindow();

                dsaKeysGeneratingWnd.Owner = Application.Current.MainWindow;

                dsaKeysGeneratingWnd.Show();
            });
        }

        public RelayCommand OpenDsaKeysGeneratingByDPWindow
        {
            get => new RelayCommand(obj =>
            {
                Window dsaKeysGeneratingWnd = new DsaKeysGeneratingByDPWindow();

                dsaKeysGeneratingWnd.Owner = Application.Current.MainWindow;

                dsaKeysGeneratingWnd.Show();
            });
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

        private bool IsValidKey(KeyType type)
        {
            if (selectedKey == null)
            {
                MessageBox.Show("Выберите ключ!");

                return false;
            }
            else if (selectedKey.KeyType != type)
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
                if (IsValidKey(KeyType.Public))
                {
                    if (SelectedKey.AlgorithmName == AlgorithmName.DSA)
                        MessageBox.Show("Алгоритм DSA не поддерживает шифрование!");
                    else if (MainTextBox == null || MainTextBox == "")
                        MessageBox.Show("Введите текст!");
                    else
                    {
                        IEncryptor encryptor = null;

                        if (SelectedKey.AlgorithmName == AlgorithmName.RSA)
                            encryptor = new RSA();
                        else if (SelectedKey.AlgorithmName == AlgorithmName.ElGamal)
                            encryptor = new ElGamal();
                        else
                            MessageBox.Show("Ключ такого типа не поддерживается!");

                        if (MainTextBox != null && MainTextBox != "")
                        {
                            byte[] bytesToEncrypt = StringToByte(MainTextBox);

                            byte[] encryptedBytes = encryptor.Encrypt(bytesToEncrypt, SelectedKey);

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
                if (IsValidKey(KeyType.Private))
                {
                    if (selectedKey.AlgorithmName == AlgorithmName.DSA)
                        MessageBox.Show("Алгоритм DSA не поддерживает шифрование!");
                    else
                    {
                        IEncryptor encryptor = null;

                        if (selectedKey.AlgorithmName == AlgorithmName.RSA)
                            encryptor = new RSA();
                        else if (selectedKey.AlgorithmName == AlgorithmName.ElGamal)
                            encryptor = new ElGamal();
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
                if (IsValidKey(KeyType.Private) && MainTextBox != null && MainTextBox != "")
                {
                    ISignatutator signatutor = null;

                    if (selectedKey.AlgorithmName == AlgorithmName.RSA)
                        signatutor = new RSA(SelectedKey.HashAlgorithm);
                    else if (selectedKey.AlgorithmName == AlgorithmName.ElGamal)
                        signatutor = new ElGamal(SelectedKey.HashAlgorithm);
                    else if (selectedKey.AlgorithmName == AlgorithmName.DSA)
                    {
                        signatutor = new DSA(SelectedKey.HashAlgorithm);
                    }
                    else
                        MessageBox.Show("Ключ такого типа не поддерживается!");

                    var bytesToCreateSignature = StringToByte(MainTextBox);

                    var sign = signatutor.CreateSignature(bytesToCreateSignature, SelectedKey);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.Filter = "XML-File | *.xml";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        DigitalSignatureXmlWriter xmlWriter = new DigitalSignatureXmlWriter();

                        sign.Accept(xmlWriter);

                        xmlWriter.Save(saveFileDialog.FileName);

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
                if (IsValidKey(KeyType.Public) && MainTextBox != null && MainTextBox != "")
                {
                    byte[] bytesToVerificate = StringToByte(MainTextBox);

                    OpenFileDialog openFileDialog = new OpenFileDialog();

                    openFileDialog.Filter = "XML-File | *.xml";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        ISignatutator signatutator = null;

                        DigitalSignature signature = null;

                        if (selectedKey.AlgorithmName == AlgorithmName.RSA)
                        {
                            signatutator = new RSA(SelectedKey.HashAlgorithm);

                            signature = new RsaDigitalSignature(0);
                        }
                        else
                        {
                            signature = new ElGamalDigitalSignature(0, 0);

                            if (selectedKey.AlgorithmName == AlgorithmName.DSA)
                            {
                                signatutator = new DSA(SelectedKey.HashAlgorithm);
                            }
                            else if (selectedKey.AlgorithmName == AlgorithmName.ElGamal)
                                signatutator = new ElGamal(SelectedKey.HashAlgorithm);
                        }

                        signature.Accept(new DigitalSignatureXmlReader(openFileDialog.FileName));

                        bool isCorrect = signatutator.VerifyDigitalSignature(signature, bytesToVerificate, selectedKey);

                        if (isCorrect)
                            MessageBox.Show("Подпись верна!");
                        else
                            MessageBox.Show("Подпись неверна!");
                    }
                }
                else
                    MessageBox.Show("Введите текст или выберите открытый ключ!");
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
