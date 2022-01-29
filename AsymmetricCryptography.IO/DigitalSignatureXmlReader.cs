using AsymmetricCryptography.DataUnits.DigitalSignatures;
using System.Numerics;
using System.Xml.Linq;

namespace AsymmetricCryptography.IO
{
    public sealed class DigitalSignatureXmlReader : IDigitalSignatureVisitor
    {
        private string FilePath { get; set; }

        public DigitalSignatureXmlReader(string filePath)
        {
            ChangeFile(filePath);
        }

        public void ChangeFile(string newFilePath)
        {
            FilePath = newFilePath;
        }

        public void VisitElGamalDigitalSignature(ElGamalDigitalSignature elGamalDigitalSignature)
        {
            XElement digitalSignature = XElement.Load(FilePath);

            string signType = digitalSignature.Attribute("SignatureType").Value;

            if (signType == "ElGamal" || signType == "DSA")
            {
                elGamalDigitalSignature.R = BigInteger.Parse(digitalSignature.Element("R").Value);
                elGamalDigitalSignature.S = BigInteger.Parse(digitalSignature.Element("S").Value);
            }
            else
                throw new ArgumentException();
        }

        public void VisitRsaDigitalSignature(RsaDigitalSignature rsaDigitalSignature)
        {
            XElement digitalSignature = XElement.Load(FilePath);

            string signType = digitalSignature.Attribute("SignatureType").Value;

            if (signType == "RSA")
            {
                rsaDigitalSignature.SignValue = BigInteger.Parse(digitalSignature.Element("SignValue").Value);
            }
            else
                throw new ArgumentException();
        }
    }
}
