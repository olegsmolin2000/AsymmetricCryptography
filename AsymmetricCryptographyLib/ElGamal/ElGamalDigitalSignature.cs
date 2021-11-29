using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Xml.Linq;

namespace AsymmetricCryptography.ElGamal
{
    //цифровая подпись по схеме Эль Гамаля
    public class ElGamalDigitalSignature :DigitalSignature
    {
        public override string Type => "El Gamal scheme digital signature";

        public BigInteger R { get; private set; }
        public BigInteger S { get; private set; }

        public ElGamalDigitalSignature(string filePath)
        {
            ReadXml(filePath);
        }

        public ElGamalDigitalSignature(BigInteger r, BigInteger s)
        {
            this.R = r;
            this.S = s;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("R: " + R + "\n");
            result.Append("S: " + S);

            return result.ToString();
        }

        public override void WriteXml(string filePath)
        {
            XDocument xDocument = new XDocument();

            XElement xSignature = new XElement("DigitalSignature");

            XAttribute xSignType = new XAttribute("SignatureType", "ElGamal");

            XElement xR = new XElement("R", R.ToString());
            XElement xS = new XElement("S", S.ToString());

            xSignature.Add(xSignType);
            xSignature.Add(xR);
            xSignature.Add(xS);

            xDocument.Add(xSignature);

            xDocument.Save(filePath);
        }

        public override void ReadXml(string filePath)
        {
            XElement digitalSignature = XElement.Load(filePath);

            string signType = digitalSignature.Attribute("SignatureType").Value;

            if (signType == "ElGamal" || signType == "DSA") 
            {
                R = BigInteger.Parse(digitalSignature.Element("R").Value);
                S = BigInteger.Parse(digitalSignature.Element("S").Value);
            }
            else
                throw new ArgumentException();
        }
    }
}
