using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    abstract class KeysGenerator
    {
        public abstract void GenerateKeyPair(int binarySize, out AsymmetricKey privateKey, out AsymmetricKey publicKey); 
    }
}
