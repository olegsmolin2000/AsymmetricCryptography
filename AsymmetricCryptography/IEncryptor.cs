using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    interface IEncryptor
    {
        public byte[] Encrypt(byte[] data);
        public byte[] Decrypt(byte[] encryptedData);
    }
}
