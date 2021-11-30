using AsymmetricCryptographyDAL.EFCore;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace AsymmetricCryptographyDAL.Entities.Keys
{
    public abstract class AsymmetricKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlgorithmName { get; private set; }
        public string Type { get; private set; }
        public int BinarySize { get; private set; }

        public string NumberGenerator { get; set; }
        public string PrimalityVerificator { get; set; }
        public string HashAlgorithm { get; set; }

        protected AsymmetricKey(string name, string algorithmName, string type, int binarySize)
        {
            this.Name = name;
            this.AlgorithmName = algorithmName;
            this.Type = type;
            this.BinarySize = binarySize;
        }

        public abstract override string ToString();

        public void SetGeneratingParameters(string[] parameters)
        {
            NumberGenerator = parameters[0];
            PrimalityVerificator = parameters[1];
            HashAlgorithm = parameters[2];
        }

        public string[] GetParametersInfo()
        {
            string[] parameters = new string[3];

            parameters[0] = NumberGenerator;
            parameters[1] = PrimalityVerificator;
            parameters[2] = HashAlgorithm;

            return parameters;
        }

        public string GetInfo()
        {
            StringBuilder info = new StringBuilder();

            info.Append("Name:" + Name + "\n");
            info.Append("AlgorithmName:" + AlgorithmName + "\n");
            info.Append("Type:" + Type + "\n");
            info.Append("BinarySize:" + BinarySize + "\n");

            info.Append("NumberGenerator:" + NumberGenerator + "\n");
            info.Append("PrimalityVerificator:" + PrimalityVerificator + "\n");
            info.Append("HashAlgorithm:" + HashAlgorithm + "\n");

            return info.ToString();
        }

        public abstract void Accept(IKeyVisitor keyVisitor); 

        public static AsymmetricKey ReadXml(XElement xKey)
        {
            AsymmetricKey key = null;

            XElement xBaseInfo = xKey.Element("BaseInformation");

            string name = xBaseInfo.Element("Name").Value;
            string algName = xBaseInfo.Element("AlgorithmName").Value;
            string type = xBaseInfo.Element("Type").Value;
            int binarySize = int.Parse(xBaseInfo.Element("BinarySize").Value);

            XElement xGenerationParameters = xBaseInfo.Element("GenerationParameters");

            string numberGenerator = xGenerationParameters.Element("NumberGenerator").Value;
            string primalityVerificator = xGenerationParameters.Element("PrimalityVerificator").Value;
            string hashAlgorithm = xGenerationParameters.Element("HashAlgorithm").Value;

            string[] generationParameters = new string[3];

            generationParameters[0] = numberGenerator;
            generationParameters[1] = primalityVerificator;
            generationParameters[2] = hashAlgorithm;

            if (algName == "RSA")
            {
                BigInteger modulus = BigInteger.Parse(xKey.Element("Modulus").Value);
                BigInteger exponent = new BigInteger();

                if (type == "Private")
                {
                    exponent = BigInteger.Parse(xKey.Element("PrivateExponent").Value);

                    key = new RsaPrivateKey(name, binarySize, modulus, exponent);
                }
                else if (type == "Public")
                {
                    exponent = BigInteger.Parse(xKey.Element("PublicExponent").Value);

                    key = new RsaPublicKey(name, binarySize, modulus, exponent);
                }
            }
            else if (algName == "ElGamal")
            {
                BigInteger p = BigInteger.Parse(xKey.Element("P").Value);
                BigInteger g = BigInteger.Parse(xKey.Element("G").Value);

                BigInteger keyValue;

                if (type == "Private")
                {
                    keyValue = BigInteger.Parse(xKey.Element("X").Value);

                    key = new ElGamalPrivateKey(name, binarySize, keyValue, p, g);
                }
                else if (type == "Public")
                {
                    keyValue = BigInteger.Parse(xKey.Element("Y").Value);

                    key = new ElGamalPublicKey(name, binarySize, keyValue, p, g);
                }
            }
            else if (algName == "DSA")
            {
                if(type== "Parameters")
                {
                    BigInteger q = BigInteger.Parse(xKey.Element("Q").Value);
                    BigInteger p = BigInteger.Parse(xKey.Element("P").Value);
                    BigInteger g = BigInteger.Parse(xKey.Element("G").Value);

                    key = new DsaDomainParameter(name, binarySize, q, p, g);
                }
                else
                {
                    XElement xDomainParameter = xKey.Element("DsaDomainParameter");

                    DsaDomainParameter domainParameter = ReadXml(xDomainParameter) as DsaDomainParameter;

                    if (!DataWorker.ContainsKey(domainParameter.Name))
                    {
                        DataWorker.AddKey(domainParameter);

                        domainParameter = DataWorker.GetLastDomainParameter() as DsaDomainParameter;
                    }
                    else
                        domainParameter = DataWorker.GetDsaDomainParameter(domainParameter.Name);

                    BigInteger keyValue = new BigInteger();

                    if (type == "Private")
                    {
                        keyValue = BigInteger.Parse(xKey.Element("X").Value);

                        key = new DsaPrivateKey(name, binarySize, domainParameter, keyValue);
                    }
                    else if (type == "Public")
                    {
                        keyValue = BigInteger.Parse(xKey.Element("Y").Value);

                        key = new DsaPublicKey(name, binarySize, domainParameter, keyValue);
                    }
                }
            }

            key.SetGeneratingParameters(generationParameters);

            return key;
        }
    }
}
