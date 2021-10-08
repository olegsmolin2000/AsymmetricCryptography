using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    abstract class AsymmetricKey
    {
        public abstract string KeyType { get; }

        public abstract void PrintConsole();
    }
}
