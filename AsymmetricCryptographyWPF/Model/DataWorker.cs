using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetricCryptographyWPF.Model
{
    static class DataWorker
    {
        public static List<Key> GetAllKeys()
        {
            using (CryptographyContext db = new CryptographyContext())
            {
                return db.Keys.ToList();
            }
        }

        public static List<RsaPrivateKey> GetAllRsaPrivateKeys()
        {
            using (CryptographyContext db = new CryptographyContext())
            {
                return db.RsaPrivateKeys.ToList();
            }
        }

        public static List<RsaPublicKey> GetAllRsaPublicKeys()
        {
            using (CryptographyContext db = new CryptographyContext())
            {
                return db.RsaPublicKeys.ToList();
            }
        }

        public static string CreateRsaPrivateKey(string name, string algorithmName, string permission, int binarySize, string modulus, string privateExponent)
        {
            using(CryptographyContext db=new CryptographyContext())
            {
                bool isAlreadyExist = db.Keys.Any(el => el.Name == name);

                if (!isAlreadyExist)
                {
                    Key key = new Key()
                    {
                        Name = name,
                        AlgorithmName = algorithmName,
                        Permission = permission,
                        BinarySize = binarySize
                    };

                    RsaPrivateKey privateKey = new RsaPrivateKey()
                    {
                        Modulus = modulus,
                        PrivateExponent = privateExponent,
                        Key=key
                    };

                    db.Keys.Add(key);
                    db.RsaPrivateKeys.Add(privateKey);

                    db.SaveChanges();

                    return "Ключ успешно добавлен";
                }

                return "Не получилось";
            }
        }

        public static string CreateRsaPublicKey(string name, string algorithmName, string permission, int binarySize, string modulus, string publicExponent)
        {
            using (CryptographyContext db = new CryptographyContext())
            {
                bool isAlreadyExist = db.Keys.Any(el => el.Name == name);

                if (!isAlreadyExist)
                {
                    Key key = new Key()
                    {
                        Name = name,
                        AlgorithmName = algorithmName,
                        Permission = permission,
                        BinarySize = binarySize
                    };

                    RsaPublicKey publicKey = new RsaPublicKey()
                    {
                        Modulus = modulus,
                        PublicExponent = publicExponent,
                        Key = key
                    };

                    db.Keys.Add(key);
                    db.RsaPublicKeys.Add(publicKey);

                    db.SaveChanges();

                    return "Ключ успешно добавлен";
                }

                return "Не получилось";
            }
        }

        public static object GetKeyById(int id)
        {
            using(CryptographyContext db=new CryptographyContext())
            {
                RsaPrivateKey rsaPrivateKey = db.RsaPrivateKeys.FirstOrDefault(key => key.KeyId == id);
                if (rsaPrivateKey != null)
                    return rsaPrivateKey;

                RsaPublicKey rsaPublicKey = db.RsaPublicKeys.FirstOrDefault(key => key.KeyId == id);
                if (rsaPublicKey != null)
                    return rsaPublicKey;
            }

            return null;
        }

        public static RsaPrivateKey GetRsaPrivateKeyById(int id)
        {
            using (CryptographyContext db = new CryptographyContext())
            {
                return db.RsaPrivateKeys.FirstOrDefault(x => x.KeyId == id);
            }
        }

        public static RsaPublicKey GetRsaPublicKeyById(int id)
        {
            using (CryptographyContext db = new CryptographyContext())
            {
                return db.RsaPublicKeys.FirstOrDefault(x => x.KeyId == id);
            }
        }
    }
}
