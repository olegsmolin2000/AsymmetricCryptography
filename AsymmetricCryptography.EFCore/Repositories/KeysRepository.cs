using AsymmetricCryptography.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace AsymmetricCryptography.EFCore.Repositories
{
    public class KeysRepository<T> : IRepository<T> where T : AsymmetricKey
    {
        private readonly KeysContext Db;
        private readonly DbSet<T> Set;

        public KeysRepository(KeysContext db)
        {
            Db = db;
            Set = db.Set<T>();
        }

        public IEnumerable<T> Items => Set.ToList();

        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (Set.Contains(item))
                throw new ArgumentException("DB contains this key");

            Set.Add(item);

            Db.SaveChanges();
        }

        public T Get(int id)
        {
            var key = Items.FirstOrDefault(item => item.Id == id);

            if (key is null)
                throw new ArgumentException("Incorrect id");
            else
                return key;
        }
    }
}
