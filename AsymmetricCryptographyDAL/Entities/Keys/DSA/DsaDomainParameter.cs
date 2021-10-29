using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.DSA
{
    public class DsaDomainParameter
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string AlgorithmName { get; private set; }
        public string Type { get; private set; }
        public int BinarySize { get; private set; }

        public BigInteger Q { get; private set; }
        public BigInteger P { get; private set; }
        public BigInteger G { get; private set; }

        public List<DsaPrivateKey> PrivateKeys { get; set; }
        public List<DsaPublicKey> PublicKeys { get; set; }

        private DsaDomainParameter(BigInteger q,BigInteger p,BigInteger g)
        {
            this.AlgorithmName = "DSA";
            this.Type = "DomainParameter";

            this.Q = q;
            this.P = p;
            this.G = g;
        }

        public DsaDomainParameter(string name,int binarySize ,BigInteger q,BigInteger p,BigInteger g)
            :this(q,p,g)
        {
            this.Name = name;
            this.BinarySize = binarySize;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Name:" + Name + "\n");
            result.Append("AlgorithmName:" + AlgorithmName + "\n");
            result.Append("Type:" + Type + "\n");
            result.Append("BinarySize:" + BinarySize + "\n");

            result.Append("Q:" + Q + "\n");
            result.Append("P:" + P + "\n");
            result.Append("G:" + G + "\n");

            return result.ToString();
        }
    }
}
