using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using System.Xml.Linq;

namespace AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors
{
    public class XmlKeyVisitor : IKeyVisitor
    {
        public XDocument xDoc { get; private set; }

        private XElement xRoot;

        public void VisitAsymmetricKey(AsymmetricKey asymmetricKey)
        {
            xRoot = new XElement("Key");

            XElement BaseInfo = new XElement("BaseInformation");

            BaseInfo.Add(new XElement("Name", asymmetricKey.Name));
            BaseInfo.Add(new XElement("AlgorithmName", asymmetricKey.AlgorithmName));
            BaseInfo.Add(new XElement("Type", asymmetricKey.Type));
            BaseInfo.Add(new XElement("BinarySize", asymmetricKey.BinarySize));

            XElement generationParameters = new XElement("GenerationParameters");

            generationParameters.Add(new XElement("NumberGenerator", asymmetricKey.NumberGenerator));
            generationParameters.Add(new XElement("PrimalityVerificator", asymmetricKey.PrimalityVerificator));
            generationParameters.Add(new XElement("HashAlgorithm", asymmetricKey.HashAlgorithm));

            BaseInfo.Add(generationParameters);

            xRoot.Add(BaseInfo);

            xDoc = new XDocument();

            xDoc.Add(xRoot);
        }

        public void VisitDsaDomainParameters(DsaDomainParameter dsaDomainParameter)
        {
            VisitAsymmetricKey(dsaDomainParameter);

            XElement q = new XElement("Q", dsaDomainParameter.Q);
            XElement p = new XElement("P", dsaDomainParameter.P);
            XElement g = new XElement("G", dsaDomainParameter.G);

            xRoot.Add(q);
            xRoot.Add(p);
            xRoot.Add(g);
        }

        private XElement GetDsaDomainParameter(AsymmetricKey dsaKey)
        {
            DsaDomainParameter domainParameter;

            if (dsaKey is DsaPrivateKey)
            {
                int domainParameterId = (int)(dsaKey as DsaPrivateKey).DomainParameterId;

                domainParameter = DataWorker.GetKey(domainParameterId) as DsaDomainParameter;
            }
            else if(dsaKey is DsaPublicKey)
            {
                int domainParameterId = (int)(dsaKey as DsaPublicKey).DomainParameterId;

                domainParameter = DataWorker.GetKey(domainParameterId) as DsaDomainParameter;
            }
            else
                return null;

            XElement xDsaDomainParameter = new XElement("DsaDomainParameter");

            XElement BaseInfo = new XElement("BaseInformation");

            BaseInfo.Add(new XElement("Name", domainParameter.Name));
            BaseInfo.Add(new XElement("AlgorithmName", domainParameter.AlgorithmName));
            BaseInfo.Add(new XElement("Type", domainParameter.Type));
            BaseInfo.Add(new XElement("BinarySize", domainParameter.BinarySize));

            XElement generationParameters = new XElement("GenerationParameters");

            generationParameters.Add(new XElement("NumberGenerator", domainParameter.NumberGenerator));
            generationParameters.Add(new XElement("PrimalityVerificator", domainParameter.PrimalityVerificator));
            generationParameters.Add(new XElement("HashAlgorithm", domainParameter.HashAlgorithm));

            BaseInfo.Add(generationParameters);

            xDsaDomainParameter.Add(BaseInfo);

            xDsaDomainParameter.Add(new XElement("Q", domainParameter.Q));
            xDsaDomainParameter.Add(new XElement("P", domainParameter.P));
            xDsaDomainParameter.Add(new XElement("G", domainParameter.G));

            return xDsaDomainParameter;
        }

        public void VisitDsaPrivateKey(DsaPrivateKey dsaPrivateKey)
        {
            VisitAsymmetricKey(dsaPrivateKey);

            var xDomainParameter = GetDsaDomainParameter(dsaPrivateKey);

            XElement x = new XElement("X",dsaPrivateKey.X);

            xRoot.Add(x);
            xRoot.Add(xDomainParameter);
        }

        public void VisitDsaPublicKey(DsaPublicKey dsaPublicKey)
        {
            VisitAsymmetricKey(dsaPublicKey);

            var xDomainParameter = GetDsaDomainParameter(dsaPublicKey);

            XElement y = new XElement("Y", dsaPublicKey.Y);

            xRoot.Add(y);
            xRoot.Add(xDomainParameter);
        }

        public void VisitElGamalPrivateKey(ElGamalPrivateKey elGamalPrivateKey)
        {
            VisitAsymmetricKey(elGamalPrivateKey);

            XElement p = new XElement("P", elGamalPrivateKey.P);
            XElement g = new XElement("G", elGamalPrivateKey.G);

            XElement x = new XElement("X", elGamalPrivateKey.X);

            xRoot.Add(p);
            xRoot.Add(g);
            xRoot.Add(x);
        }

        public void VisitElGamalPublicKey(ElGamalPublicKey elGamalPublicKey)
        {
            VisitAsymmetricKey(elGamalPublicKey);

            XElement p = new XElement("P", elGamalPublicKey.P);
            XElement g = new XElement("G", elGamalPublicKey.G);

            XElement y = new XElement("Y", elGamalPublicKey.Y);

            xRoot.Add(p);
            xRoot.Add(g);
            xRoot.Add(y);
        }

        public void VisitRsaPrivateKey(RsaPrivateKey rsaPrivateKey)
        {
            VisitAsymmetricKey(rsaPrivateKey);

            XElement modulus = new XElement("Modulus", rsaPrivateKey.Modulus);
            XElement privateExponent = new XElement("PrivateExponent", rsaPrivateKey.PrivateExponent);

            xRoot.Add(modulus);
            xRoot.Add(privateExponent);
        }

        public void VisitRsaPublicKey(RsaPublicKey rsaPublicKey)
        {
            VisitAsymmetricKey(rsaPublicKey);

            XElement modulus = new XElement("Modulus", rsaPublicKey.Modulus);
            XElement publicExponent = new XElement("PublicExponent", rsaPublicKey.PublicExponent);

            xRoot.Add(modulus);
            xRoot.Add(publicExponent);
        }
    }
}
