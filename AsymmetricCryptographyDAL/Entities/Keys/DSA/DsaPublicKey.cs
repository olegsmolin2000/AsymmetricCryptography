using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.DSA
{
    public class DsaPublicKey : AsymmetricKey
    {
        public BigInteger Y { get; private set; }

        public virtual DsaDomainParameter DomainParameter { get; set; }

        private DsaPublicKey(string name, int binarySize)
            : base(name, "DSA", "Public", binarySize) { }

        public DsaPublicKey(string name, int binarySize, DsaDomainParameter domainParameter, BigInteger y)
            : this(name, binarySize)
        {
            this.Y = y;

            this.DomainParameter = domainParameter;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("\nDomainParameter:\n");
            result.Append(DomainParameter.ToString());
            result.Append("\n");

            result.Append("Y:" + Y + "\n");

            return result.ToString();
        }
    }
}
