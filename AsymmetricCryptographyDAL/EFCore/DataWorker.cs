using AsymmetricCryptographyDAL.EFCore.Contexts;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void ClearDB()
        {
            using (KeyContext db = new KeyContext())
            {
                foreach(var el in db.Keys)
                {
                    db.Keys.Remove(el);
                }

                db.SaveChanges();
            }
        }

        public static List<DsaDomainParameter> GetDsaDomainParameters()
        {
            using(KeyContext db=new KeyContext())
            {
                return db.DsaDomainParameters.ToList();
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

        public static DsaDomainParameter GetDsaDomainParameter(string name)
        {
            using (KeyContext db = new KeyContext())
            {
                DsaDomainParameter key = db.DsaDomainParameters.FirstOrDefault(o => o.Name == name);

                return key;
            }
        }

        public static bool ContainsKey(string name)
        {
            using (KeyContext db = new KeyContext())
            {
                if (db.Keys.FirstOrDefault(o=>o.Name==name)!=null)
                    return true;
                return false;
            }
        }

        public static void AddKey(AsymmetricKey key)
        {
            using(KeyContext db=new KeyContext())
            {
                if (!db.Keys.Contains(key))
                {
                    db.Keys.Add(key);

                    db.SaveChanges();
                }
            }
        }

        public static AsymmetricKey GetLastDomainParameter()
        {
            using (KeyContext db = new KeyContext())
            {
                int lastId = db.DsaDomainParameters.Max(x => x.Id);

                return db.DsaDomainParameters.FirstOrDefault(x => x.Id == lastId);
            }
        }
    }
}
