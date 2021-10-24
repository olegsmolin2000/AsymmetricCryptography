using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    public abstract class DigitalSignature
    {
        public virtual string Type { get; }

        public abstract void PrintConsole();
    }
}
