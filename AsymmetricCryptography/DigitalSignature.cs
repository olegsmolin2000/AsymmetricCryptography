using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    abstract class DigitalSignature
    {
        public virtual string Type { get; }

        public abstract void PrintConsole();
    }
}
