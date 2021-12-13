namespace AsymmetricCryptography.Core.NumbersGenerating
{

    public sealed class FibonacciNumberGenerator : NumberGenerator
    {
        
        private static readonly List<(int, int)> Laggs = new List<(int, int)>()
        {
            (24, 55), (38, 89), (37, 100), (30, 127),
            (83, 258), (107, 378), (273, 607), (1029, 2281),
            (576, 3217), (4187, 9689), (7083, 19937), (9739, 23209)
        };
        
        public override BigInteger GenerateNumber(int binarySize)
        {
            throw new NotImplementedException();
        }
    }
}
