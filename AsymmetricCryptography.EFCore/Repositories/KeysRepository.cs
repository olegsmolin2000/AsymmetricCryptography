using AsymmetricCryptography.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace AsymmetricCryptography.EFCore.Repositories
{
    public class KeysRepository<T> : IRepository<T> where T : AsymmetricKey
    {
        private readonly KeysContext Db;
        private readonly DbSet<T> Set;

        public KeysRepository()
        {
            Db = new KeysContext();
            Set = Db.Set<T>();
        }

        public List<T> Items => Set.ToList();

        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (!Db.Keys.Contains(item))
            {
                Set.Add(item);

                Db.SaveChanges();
            }
        }

        public T Get(int id)
        {
            var key = Items.FirstOrDefault(item => item.Id == id);

            if (key is null)
                throw new ArgumentException("Incorrect id");
            else
                return key;
        }

        public T Get(string name)
        {
            var key = Items.FirstOrDefault(item => item.Name == name);

            if (key is null)
                throw new ArgumentException("Incorrect id");
            else
                return key;
        }
    }
}
