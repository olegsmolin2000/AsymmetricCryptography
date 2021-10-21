using System;
using System.Collections.Generic;
using System.Text;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography
{
    interface IDigitalSignatutator
    {
        public DigitalSignature CreateSignature(byte[] data, CryptographicHashAlgorithm hashAlgorithm);
        public bool VerifyDigitalSignature(DigitalSignature signature, byte[] data, CryptographicHashAlgorithm hashAlgorithm);
    }
}
