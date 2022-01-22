namespace AsymmetricCryptography.EFCore.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> Items { get; }

        T Get(int id);

        void Add(T item);
    }
}
