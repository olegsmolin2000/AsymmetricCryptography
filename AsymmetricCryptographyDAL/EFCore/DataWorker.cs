using AsymmetricCryptographyDAL.EFCore.Contexts;
using AsymmetricCryptographyDAL.Entities.Keys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetricCryptographyDAL.EFCore
{
    public static class DataWorker
    {
        public static List<AsymmetricKey> GetAll()
        {
            using (KeyContext db = new KeyContext())
            {
                return db.Keys.ToList();
            }
        }

        public static AsymmetricKey GetKey(int id)
        {
            using (KeyContext db = new KeyContext())
            {
                AsymmetricKey key = db.Keys.FirstOrDefault(o => o.Id == id);

                return key;
            }
        }

        public static void AddKey<T>(T key) where T: class
        {
            using(KeyContext db=new KeyContext())
            {
                var set = db.Set<T>();

                if (!set.Contains(key))
                {
                    set.Add(key);

                    db.SaveChanges();
                }
            }
        }
    }
}
