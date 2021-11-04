using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.DSA
{
    public class DsaPublicKey:AsymmetricKey
    {
        public BigInteger Y { get; private set; }

        public int DomainParameterId { get; set; }
        public DsaDomainParameter DomainParameter { get; set; }

        private DsaPublicKey(string name, int binarySize, string[] generationParameters)
            : base(name, "DSA", "Public", binarySize, generationParameters) { }

        public DsaPublicKey(string name, int binarySize,string[] generationParameters, BigInteger y)
            : this(name, binarySize, generationParameters)
        {
            this.Y = y;
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
