using AsymmetricCryptography.DataUnits;
using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using AsymmetricCryptography.DataUnits.Keys.RSA;
using System.Numerics;
using System.Xml.Linq;

namespace AsymmetricCryptography.IO
{
    public sealed class XmlKeyReader
    {
        public AsymmetricKey ReadXml(string filePath)
        {
            return ReadXml(XElement.Load(filePath));
        }

      private AsymmetricKey ReadXml(XElement xKey)
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

            if (algName == "RSA")
            {
                BigInteger modulus = BigInteger.Parse(xKey.Element("Modulus").Value);
                BigInteger exponent = new BigInteger();

                if (type == "Private")
                {
                    exponent = BigInteger.Parse(xKey.Element("PrivateExponent").Value);

                    key = new RsaPrivateKey(binarySize, exponent, modulus);
                }
                else if (type == "Public")
                {
                    exponent = BigInteger.Parse(xKey.Element("PublicExponent").Value);

                    key = new RsaPublicKey(binarySize, exponent, modulus);
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

                    key = new ElGamalPrivateKey(binarySize, p, g, keyValue);
                }
                else if (type == "Public")
                {
                    keyValue = BigInteger.Parse(xKey.Element("Y").Value);

                    key = new ElGamalPublicKey(binarySize, p, g, keyValue);
                }
            }
            else if (algName == "DSA")
            {
                if (type == "DomainParameter")
                {
                    BigInteger q = BigInteger.Parse(xKey.Element("Q").Value);
                    BigInteger p = BigInteger.Parse(xKey.Element("P").Value);
                    BigInteger g = BigInteger.Parse(xKey.Element("G").Value);

                    key = new DsaDomainParameter(binarySize, q, p, g);
                }
                else
                {
                    XElement xDomainParameter = xKey.Element("DsaDomainParameter");

                    DsaDomainParameter domainParameter = ReadXml(xDomainParameter) as DsaDomainParameter;

                    BigInteger keyValue = new BigInteger();

                    if (type == "Private")
                    {
                        keyValue = BigInteger.Parse(xKey.Element("X").Value);

                        key = new DsaPrivateKey(binarySize, keyValue);
                        ((DsaPrivateKey)key).DomainParameter = domainParameter;

                    }
                    else if (type == "Public")
                    {
                        keyValue = BigInteger.Parse(xKey.Element("Y").Value);

                        key = new DsaPublicKey(binarySize, keyValue);

                        ((DsaPublicKey)key).DomainParameter = domainParameter;
                    }
                }
            }

            key.Name = name;

            key.NumberGenerator = Enum.Parse<RandomNumberGenerator>(numberGenerator);
            key.PrimalityVerificator = Enum.Parse<PrimalityTest>(primalityVerificator);
            key.HashAlgorithm = Enum.Parse<CryptographicHashAlgorithm>(hashAlgorithm);

            return key;
        }
    }
}
