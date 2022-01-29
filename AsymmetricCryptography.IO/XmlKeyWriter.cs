using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using AsymmetricCryptography.DataUnits.Keys.RSA;
using System.Xml.Linq;

namespace AsymmetricCryptography.IO
{
    public class XmlKeyWriter : IKeyVisitor
    {
        private XDocument xDocument { get; set; }
        private XElement xRoot { get; set; }

        public XmlKeyWriter()
        {
            Clear();
        }

        private void Clear()
        {
            xDocument = new XDocument();

            xRoot = new XElement("AsymmetricKey");

            xDocument.Add(xRoot);
        }

        private XElement GetBaseInfo(AsymmetricKey asymmetricKey)
        {
            XElement baseInfo = new XElement("BaseInformation");

            baseInfo.Add(new XElement("Name", asymmetricKey.Name));
            baseInfo.Add(new XElement("AlgorithmName", asymmetricKey.AlgorithmName));
            baseInfo.Add(new XElement("Type", asymmetricKey.KeyType));
            baseInfo.Add(new XElement("BinarySize", asymmetricKey.BinarySize));

            XElement generationParameters = new XElement("GenerationParameters");

            generationParameters.Add(new XElement("NumberGenerator", asymmetricKey.NumberGenerator));
            generationParameters.Add(new XElement("PrimalityVerificator", asymmetricKey.PrimalityVerificator));
            generationParameters.Add(new XElement("HashAlgorithm", asymmetricKey.HashAlgorithm));

            baseInfo.Add(generationParameters);

            return baseInfo;
        }

        private XElement GetDsaDomainParameter(DsaDomainParameter domainParameter)
        {
            XElement xDomainParameter = new XElement("DsaDomainParameter");

            xDomainParameter.Add(GetBaseInfo(domainParameter));

            xDomainParameter.Add(new XElement("Q", domainParameter.Q));
            xDomainParameter.Add(new XElement("P", domainParameter.P));
            xDomainParameter.Add(new XElement("G", domainParameter.G));

            return xDomainParameter;
        }

        public void VisitDsaDomainParameters(DsaDomainParameter dsaDomainParameter)
        {
            Clear();

            xRoot.Add(GetDsaDomainParameter(dsaDomainParameter));
        }

        public void VisitDsaPrivateKey(DsaPrivateKey dsaPrivateKey)
        {
            Clear();

            xRoot.Add(GetBaseInfo(dsaPrivateKey));

            xRoot.Add(new XElement("X", dsaPrivateKey.X));

            xRoot.Add(GetDsaDomainParameter(dsaPrivateKey.DomainParameter));
        }

        public void VisitDsaPublicKey(DsaPublicKey dsaPublicKey)
        {
            Clear();

            xRoot.Add(GetBaseInfo(dsaPublicKey));

            xRoot.Add(new XElement("Y", dsaPublicKey.Y));

            xRoot.Add(GetDsaDomainParameter(dsaPublicKey.DomainParameter));
        }

        public void VisitElGamalPrivateKey(ElGamalPrivateKey elGamalPrivateKey)
        {
            Clear();

            xRoot.Add(GetBaseInfo(elGamalPrivateKey));

            xRoot.Add(new XElement("P", elGamalPrivateKey.P));
            xRoot.Add(new XElement("G", elGamalPrivateKey.G));
            xRoot.Add(new XElement("X", elGamalPrivateKey.X));
        }

        public void VisitElGamalPublicKey(ElGamalPublicKey elGamalPublicKey)
        {
            Clear();

            xRoot.Add(GetBaseInfo(elGamalPublicKey));

            xRoot.Add(new XElement("P", elGamalPublicKey.P));
            xRoot.Add(new XElement("G", elGamalPublicKey.G));
            xRoot.Add(new XElement("Y", elGamalPublicKey.Y));
        }

        public void VisitRsaPrivateKey(RsaPrivateKey rsaPrivateKey)
        {
            Clear();

            xRoot.Add(GetBaseInfo(rsaPrivateKey));

            xRoot.Add(new XElement("Modulus", rsaPrivateKey.Modulus));
            xRoot.Add(new XElement("PrivateExponent", rsaPrivateKey.PrivateExponent));
        }

        public void VisitRsaPublicKey(RsaPublicKey rsaPublicKey)
        {
            Clear();

            xRoot.Add(GetBaseInfo(rsaPublicKey));

            xRoot.Add(new XElement("Modulus", rsaPublicKey.Modulus));
            xRoot.Add(new XElement("PublicExponent", rsaPublicKey.PublicExponent));
        }

        public void Save(string filePath)
        {
            xDocument.Save(filePath);
        }
    }
}
