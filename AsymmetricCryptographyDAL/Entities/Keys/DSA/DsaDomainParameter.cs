using AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.DSA
{
    public class DsaDomainParameter : AsymmetricKey
    {
        public BigInteger Q { get; private set; }
        public BigInteger P { get; private set; }
        public BigInteger G { get; private set; }

        public List<DsaPrivateKey> DsaPrivateKeys { get; set; }
        public List<DsaPublicKey> DsaPublicKeys { get; set; }

        private DsaDomainParameter(string name, int binarySize)
            : base(name, "DSA", "Parameters", binarySize) { }

        public DsaDomainParameter(string name, int binarySize, BigInteger q, BigInteger p, BigInteger g)
            : this(name, binarySize)
        {
            this.Q = q;
            this.P = p;
            this.G = g;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Q:" + Q + "\n");
            result.Append("P:" + P + "\n");
            result.Append("G:" + G + "\n");

            return result.ToString();
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitDsaDomainParameters(this);
        }
    }
}
