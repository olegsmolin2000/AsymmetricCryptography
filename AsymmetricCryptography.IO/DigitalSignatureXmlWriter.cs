using AsymmetricCryptography.DataUnits.DigitalSignatures;
using System.Xml.Linq;

namespace AsymmetricCryptography.IO
{
    public class DigitalSignatureXmlWriter : IDigitalSignatureVisitor
    {
        private XDocument xDocument;
        private XElement xSignature;

        private void Clear()
        {
            xDocument = new XDocument();

            xSignature = new XElement("DigitalSignature");
        }

        public void VisitElGamalDigitalSignature(ElGamalDigitalSignature elGamalDigitalSignature)
        {
            Clear();

            xSignature.Add(new XAttribute("SignatureType", "ElGamal"));
            xSignature.Add(new XElement("R", elGamalDigitalSignature.R.ToString()));
            xSignature.Add(new XElement("S", elGamalDigitalSignature.S.ToString()));

            xDocument.Add(xSignature);
        }

        public void VisitRsaDigitalSignature(RsaDigitalSignature rsaDigitalSignature)
        {
            Clear();

            xSignature.Add(new XAttribute("SignatureType", "RSA"));
            xSignature.Add(new XElement("SignValue", rsaDigitalSignature.SignValue.ToString()));

            xDocument.Add(xSignature);
        }

        public void Save(string filepath)
        {
            xDocument.Save(filepath);
        }
    }
}
