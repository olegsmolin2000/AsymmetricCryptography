using System.Xml.Linq;

namespace AsymmetricCryptography
{
    public abstract class DigitalSignature
    {
        public virtual string Type { get; }

        public abstract override string ToString();

        public abstract void WriteXml(string filePath);
        public abstract void ReadXml(string filePath);
    }
}
