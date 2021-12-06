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
using AsymmetricCryptographyLib.RandomNumberGenerators;
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

                GeneratingParameters parameters = new GeneratingParameters(numberGenerator, primalityVerificator, new SHA_256());

                //if (i % 10 == 0)
                Console.WriteLine(i);

                AsymmetricKey rsaPrivateKey;
                AsymmetricKey rsaPublicKey;
                DsaDomainParameter dsaDomainParameters;
                AsymmetricKey dsaPrivateKey;
                AsymmetricKey dsaPublicKey;
                AsymmetricKey elGamalPrivateKey;
                AsymmetricKey elGamalPublicKey;

                KeysGenerator rsaGenerator = new RsaKeysGenerator(parameters);
                DsaKeysGenerator dsaGenerator = new DsaKeysGenerator(parameters);
                KeysGenerator elGamalGenerator = new ElGamalKeysGenerator(parameters);

                rsaGenerator.GenerateKeyPair("1", 64, out rsaPrivateKey, out rsaPublicKey);
                (rsaGenerator as RsaKeysGenerator).IsFixedPublicExponent = false;
                dsaGenerator.DsaKeysGeneration("2", 384, 256, out dsaDomainParameters, out dsaPrivateKey, out dsaPublicKey);
                elGamalGenerator.GenerateKeyPair("3", 24, out elGamalPrivateKey, out elGamalPublicKey);



                StringBuilder message = new StringBuilder();

                int mesLength = rand.Next() % 5000;

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 256)));
                }

                //string me = "zhopa\ngovnomocha\t\nhuy";

                byte[] data = Encoding.Unicode.GetBytes(message.ToString());



                RsaAlgorithm rsaAlg = new RsaAlgorithm();
                DSA dsaAlg = new DSA(dsaDomainParameters);
                ElGamalAlgorithm elGamalAlg = new ElGamalAlgorithm();

                AsymmetricAlgorithm.HashAlgorithm = new SHA_256();

                DigitalSignature rsaSign = rsaAlg.CreateSignature(data, rsaPrivateKey);
                DigitalSignature dsaSign = dsaAlg.CreateSignature(data, dsaPrivateKey);
                DigitalSignature elGamalsign = elGamalAlg.CreateSignature(data, elGamalPrivateKey);

                bool rsaVer = rsaAlg.VerifyDigitalSignature(rsaSign, data, rsaPublicKey);
                bool dsaVer = dsaAlg.VerifyDigitalSignature(dsaSign, data, dsaPublicKey);
                bool elgamalVer = elGamalAlg.VerifyDigitalSignature(elGamalsign, data, elGamalPublicKey);


                var rsaEncryption = rsaAlg.Encrypt(data, rsaPublicKey);
                var elGamalEncryption = elGamalAlg.Encrypt(data, elGamalPublicKey);

                var rsaDecryption = rsaAlg.Decrypt(rsaEncryption, rsaPrivateKey);
                var elGamalDecryption = elGamalAlg.Decrypt(elGamalEncryption, elGamalPrivateKey);



                if (!rsaVer)
                    Console.WriteLine("rsa sign error");
                if (!dsaVer)
                    Console.WriteLine("dsa sign error");
                if (!elgamalVer)
                    Console.WriteLine("el gamal sign error");

                string resStr = Encoding.Unicode.GetString(rsaDecryption);

                if (resStr != message.ToString())
                    Console.WriteLine("rsa crypt error");
                if (Encoding.Unicode.GetString(elGamalDecryption) != message.ToString())
                    Console.WriteLine("el gamal crypt error");
            }

            //    PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();

            //NumberGenerator numberGenerator = new BbsNumberGenerator(primalityVerificator);

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(BinaryConverter.BigIntToBinary(numberGenerator.GenerateNumber(64)));
            //}




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
