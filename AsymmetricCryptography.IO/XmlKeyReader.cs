using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using AsymmetricCryptography.DataUnits.Keys.RSA;
using System.Numerics;
using System.Xml.Linq;

namespace AsymmetricCryptography.IO
{
    public class XmlKeyReader : IKeyVisitor
    {
        private string FilePath { get; set; }

        private XElement root;

        public XmlKeyReader(string filePath)
        {
            FilePath = filePath;
        }

        public AsymmetricKey LoadKey(string newFilePath)
        {
            FilePath = newFilePath;

            root = XElement.Load(FilePath);

            AsymmetricKey key;

            XElement xBaseInfo = root.Element("BaseInformation");

            string name = xBaseInfo.Element("Name").Value;
            string algName = xBaseInfo.Element("AlgorithmName").Value;
            string type = xBaseInfo.Element("Type").Value;
            int binarySize = int.Parse(xBaseInfo.Element("BinarySize").Value);

            XElement xGenerationParameters = xBaseInfo.Element("GenerationParameters");

            string numberGenerator = xGenerationParameters.Element("NumberGenerator").Value;
            string primalityVerificator = xGenerationParameters.Element("PrimalityVerificator").Value;
            string hashAlgorithm = xGenerationParameters.Element("HashAlgorithm").Value;

            if (algName == "RSA")
            {
                BigInteger modulus = BigInteger.Parse(root.Element("Modulus").Value);
                BigInteger exponent = new BigInteger();

                if (type == "Private")
                {
                    exponent = BigInteger.Parse(root.Element("PrivateExponent").Value);

                    key = new RsaPrivateKey(binarySize, modulus, exponent);
                }
                else if (type == "Public")
                {
                    exponent = BigInteger.Parse(root.Element("PublicExponent").Value);

                    key = new RsaPublicKey(binarySize, modulus, exponent);
                }
            }
            else if (algName == "ElGamal")
            {
                BigInteger p = BigInteger.Parse(root.Element("P").Value);
                BigInteger g = BigInteger.Parse(root.Element("G").Value);

                BigInteger keyValue;

                if (type == "Private")
                {
                    keyValue = BigInteger.Parse(root.Element("X").Value);

                    key = new ElGamalPrivateKey(binarySize, keyValue, p, g);
                }
                else if (type == "Public")
                {
                    keyValue = BigInteger.Parse(root.Element("Y").Value);

                    key = new ElGamalPublicKey(binarySize, keyValue, p, g);
                }
            }
            else if (algName == "DSA")
            {
                if (type == "Parameters")
                {
                    BigInteger q = BigInteger.Parse(root.Element("Q").Value);
                    BigInteger p = BigInteger.Parse(root.Element("P").Value);
                    BigInteger g = BigInteger.Parse(root.Element("G").Value);

                    key = new DsaDomainParameter(binarySize, q, p, g);
                }
                else
                {
                    XElement xDomainParameter = root.Element("DsaDomainParameter");

                    //DsaDomainParameter domainParameter = ReadXml(xDomainParameter) as DsaDomainParameter;

                    //if (!DataWorker.ContainsKey(domainParameter.Name))
                    //{
                    //    DataWorker.AddKey(domainParameter);

                    //    domainParameter = DataWorker.GetLastDomainParameter() as DsaDomainParameter;
                    //}
                    //else
                    //    domainParameter = DataWorker.GetDsaDomainParameter(domainParameter.Name);

                    BigInteger keyValue = new BigInteger();

                    //if (type == "Private")
                    //{
                    //    keyValue = BigInteger.Parse(root.Element("X").Value);

                    //    key = new DsaPrivateKey(name, binarySize, domainParameter, keyValue);
                    //}
                    //else if (type == "Public")
                    //{
                    //    keyValue = BigInteger.Parse(root.Element("Y").Value);

                    //    key = new DsaPublicKey(name, binarySize, domainParameter, keyValue);
                    //}
                }
            }

            return null;
        }

        public void VisitDsaDomainParameters(DsaDomainParameter dsaDomainParameter)
        {
            throw new NotImplementedException();
        }

        public void VisitDsaPrivateKey(DsaPrivateKey dsaPrivateKey)
        {
            throw new NotImplementedException();
        }

        public void VisitDsaPublicKey(DsaPublicKey dsaPublicKey)
        {
            throw new NotImplementedException();
        }

        public void VisitElGamalPrivateKey(ElGamalPrivateKey elGamalPrivateKey)
        {
            throw new NotImplementedException();
        }

        public void VisitElGamalPublicKey(ElGamalPublicKey elGamalPublicKey)
        {
            throw new NotImplementedException();
        }

        public void VisitRsaPrivateKey(RsaPrivateKey rsaPrivateKey)
        {
            throw new NotImplementedException();
        }

        public void VisitRsaPublicKey(RsaPublicKey rsaPublicKey)
        {
            throw new NotImplementedException();
        }
    }
}
