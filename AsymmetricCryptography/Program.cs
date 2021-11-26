using System;
using System.Text;
using AsymmetricCryptography;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;
using AsymmetricCryptography.RSA;
using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
//using AsymmetricCryptographyDAL.Entities.Keys.DSA;

namespace AsymmetricCryptographyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //    PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();
            //    NumberGenerator numberGenerator = new FibonacciNumberGenerator(primalityVerificator);
            //    primalityVerificator.SetNumberGenerator(numberGenerator);

            //    Parameters parameters = new Parameters(numberGenerator, primalityVerificator, new SHA_256());

            //    AsymmetricAlgorithm rsa = new RsaAlgorithm(parameters);

            //    KeysGenerator generator = new RsaKeysGenerator(parameters);

            //    AsymmetricKey pub, priv;

            //    generator.GenerateKeyPair(64, out priv, out pub);

            //    rsa.SetKeys(null, pub);

            //    var text = Encoding.UTF8.GetBytes("zhopa asfrest 123");

            //    var encryption = (rsa as RsaAlgorithm).Encrypt(text);


            //string str;

            //BigInteger q1 = 45235235;

            //str = q1.ToString();
            //Console.WriteLine(str);

            //BigInteger q2 = BigInteger.Parse(str);



            //Console.WriteLine(q2==q1);



            Random rand = new Random();

            int s = 0;
            int count = 1000;

            for (int i = 0; i < count; i++)
            {
                PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();
                NumberGenerator numberGenerator = new FibonacciNumberGenerator(primalityVerificator);
                //primalityVerificator.SetNumberGenerator(numberGenerator);

                GeneratingParameters parameters = new GeneratingParameters(numberGenerator, primalityVerificator, new SHA_256());

                //if (i % 10 == 0)
                Console.WriteLine(i);

                AsymmetricKey rsaPrivateKey;
                AsymmetricKey rsaPublicKey;
                //DsaDomainParameter dsaDomainParameters;
                AsymmetricKey dsaPrivateKey;
                AsymmetricKey dsaPublicKey;
                AsymmetricKey elGamalPrivateKey;
                AsymmetricKey elGamalPublicKey;

                KeysGenerator rsaGenerator = new RsaKeysGenerator(parameters);
                KeysGenerator dsaGenerator = new DsaKeysGenerator(parameters);
                KeysGenerator elGamalGenerator = new ElGamalKeysGenerator(parameters);

                rsaGenerator.GenerateKeyPair("1", 64, out rsaPrivateKey, out rsaPublicKey);
                (rsaGenerator as RsaKeysGenerator).IsFixedPublicExponent = false;
                //dsaGenerator.GenerateKeyPair("2", 384, out dsaPrivateKey, out dsaPublicKey);
                //elGamalGenerator.GenerateKeyPair("3", 64, out elGamalPrivateKey, out elGamalPublicKey);



                StringBuilder message = new StringBuilder();

                int mesLength = rand.Next() % 5000;

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 255 + 1)));
                }

                string me = "zhopa\ngovnomocha\t\nhuy";

                byte[] data = Encoding.UTF8.GetBytes(me);



                RsaAlgorithm rsaAlg = new RsaAlgorithm(parameters);
                //DSA dsaAlg = new DSA(dsaPrivateKey, dsaPublicKey, parameters);
                //ElGamalAlgorithm elGamalAlg = new ElGamalAlgorithm(elGamalPrivateKey, elGamalPublicKey, parameters);

                //DigitalSignature rsaSign = rsaAlg.CreateSignature(data);
               // DigitalSignature dsaSign = dsaAlg.CreateSignature(data);
                //DigitalSignature elGamalsign = elGamalAlg.CreateSignature(data);

                //bool rsaVer = rsaAlg.VerifyDigitalSignature(rsaSign, data);
                //bool dsaVer = dsaAlg.VerifyDigitalSignature(dsaSign, data);
               // bool elgamalVer = elGamalAlg.VerifyDigitalSignature(elGamalsign, data);


                var rsaEncryption = rsaAlg.Encrypt(data,rsaPublicKey);
                //var elGamalEncryption = elGamalAlg.Encrypt(data);

                var rsaDecryption = rsaAlg.Decrypt(rsaEncryption,rsaPrivateKey);
                //var elGamalDecryption = elGamalAlg.Decrypt(elGamalEncryption);



                //if (!rsaVer)
                //  Console.WriteLine("rsa sign error");
                //if (!dsaVer)
                //  Console.WriteLine("dsa sign error");
                //if (!elgamalVer)
                //  Console.WriteLine("el gamal sign error");

                string resStr = Encoding.UTF8.GetString(rsaDecryption);

                if (resStr!=me)
                    Console.WriteLine("rsa crypt error");
                //if (Encoding.UTF8.GetString(elGamalDecryption) != message.ToString())
                //    Console.WriteLine("el gamal crypt error");


            }


            //Console.WriteLine("s:" + s);
            //Console.WriteLine("count:" + count);


            //int keySize = 0;

            //Console.Write("input key binary size:");
            //keySize = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine();
            //RsaAlgorithm rsa = new RsaAlgorithm(keySize);
            //rsa.PrintKeys();
            //string message = "";
            //Console.WriteLine();
            //Console.Write("input messsage:");
            //message = Console.ReadLine();

            //byte[] mesBytes = Encoding.UTF8.GetBytes(message);

            //byte[] encrypt = rsa.Encrypt(mesBytes);

            //Console.WriteLine();

            //Console.WriteLine("encryption:"+Encoding.UTF8.GetString(encrypt));
            //Console.WriteLine();
            //byte[] decrypt = rsa.Decrypt(encrypt);

            //Console.WriteLine("decryption:" + Encoding.UTF8.GetString(decrypt));
            //Console.WriteLine();
            //CryptographicHashAlgorithm hashAlgorithm = new SHA_256();

            //DigitalSignature sign = rsa.CreateSignature(mesBytes, hashAlgorithm);

            //sign.PrintConsole();
            //Console.WriteLine();

            //Console.WriteLine("signature verification:" + rsa.VerifyDigitalSignature(sign, mesBytes, hashAlgorithm));




            //while (true)
            //{
            //    string text = Console.ReadLine();

            //    CryptographicHashAlgorithm hashAlgorithm = new MD_5();

            //    var hash = hashAlgorithm.GetHash(Encoding.UTF8.GetBytes(text));

            //    for (int i = 0; i < hash.Length; i++)
            //    {
            //        Console.Write(hash[i].ToString("x"));
            //    }
            //    Console.WriteLine();
            //}

        }
    }
}
