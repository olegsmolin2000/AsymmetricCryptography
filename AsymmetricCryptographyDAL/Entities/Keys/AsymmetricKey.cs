namespace AsymmetricCryptographyDAL.Entities.Keys
{
    public abstract class AsymmetricKey
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string AlgorithmName { get; private set; }
        public string Permission { get; private set; }
        public int BinarySize { get; private set; }

        protected AsymmetricKey(string name,string algorithmName,string permission,int binarySize)
        {
            this.Name = name;
            this.AlgorithmName = algorithmName;
            this.Permission = permission;
            this.BinarySize = binarySize;
        }
    }
}
