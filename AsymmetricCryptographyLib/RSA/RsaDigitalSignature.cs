using System;
using System.Numerics;
using System.Xml;
using System.Xml.Linq;

namespace AsymmetricCryptography.RSA
{
    public class RsaDigitalSignature : DigitalSignature
    {
        public override string Type => "RSA digital signature";

        public BigInteger signValue { get; private set; }

        public RsaDigitalSignature(string filePath)
        {
            ReadXml(filePath);
        }

        public RsaDigitalSignature(BigInteger signValue)
        {
            this.signValue = signValue;
        }
        public override string ToString()
        {
            return new string("Sign value: " + signValue);
        }

        public override void WriteXml(string filePath)
        {
            XDocument xDocument = new XDocument();

            XElement xSignature = new XElement("DigitalSignature");

            XAttribute xSignType = new XAttribute("SignatureType", "RSA");

            XElement xValue = new XElement("SignValue", signValue.ToString());

            xSignature.Add(xSignType);
            xSignature.Add(xValue);

            xDocument.Add(xSignature);

            xDocument.Save(filePath);
        }

        public override void ReadXml(string filePath)
        {
            XElement digitalSignature = XElement.Load(filePath);

            string signType = digitalSignature.Attribute("SignatureType").Value;

            if (signType == "RSA")
            {
                signValue = BigInteger.Parse(digitalSignature.Element("SignValue").Value);
            }
            else
                throw new ArgumentException();
        }
    }
}
