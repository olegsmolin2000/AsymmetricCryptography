using System;
using System.Collections.Generic;
using System.Text;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography
{
    interface IDigitalSignatutator
    {
        public DigitalSignature CreateSignature(byte[] data);
        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data);
    }
}
