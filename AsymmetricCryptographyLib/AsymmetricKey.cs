using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    public abstract class AsymmetricKey
    {
        public abstract string KeyType { get; }

        public abstract void PrintConsole();
    }
}
