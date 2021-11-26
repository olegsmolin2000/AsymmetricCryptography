using System.Text;

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
    }
}
