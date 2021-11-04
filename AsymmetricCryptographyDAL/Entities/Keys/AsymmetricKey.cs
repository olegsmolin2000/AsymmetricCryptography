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

        public string NumberGenerator { get; private set; }
        public string PrimalityVerificator { get; private set; }
        public string HashAlgorithm { get; private set; }

        protected AsymmetricKey(string name,string algorithmName,string type, int binarySize, string[] generationParameters)
        {
            this.Name = name;
            this.AlgorithmName = algorithmName;
            this.Type = type;
            this.BinarySize = binarySize;

            this.NumberGenerator = generationParameters[0];
            this.PrimalityVerificator = generationParameters[1];
            this.HashAlgorithm = generationParameters[2];
        }

        public abstract override string ToString();

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
