using System.Text;
using System.Xml.Linq;
using AsymmetricCryptography.Core;
using AsymmetricCryptography.Core.HashAlgorithms;
using AsymmetricCryptography.Core.KeysGenerators;
using AsymmetricCryptography.Core.NumberGenerators;
using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits.Keys;

//var dp1 = new DsaDomainParameter(1, 1, 1, 1)
//{
//    Name = "dp1"
//};

//var dp2 = new DsaDomainParameter(2, 2, 2, 2)
//{
//    Name = "dp2"
//};

//var q1 = new DsaPrivateKey(25, 1, dp1)
//{
//    Name = "q1"
//};

//var q2 = new DsaPrivateKey(20, 2, dp2)
//{
//    Name = "q2"
//};
//var q3 = new DsaPrivateKey(14, 3, dp1)
//{
//    Name = "q3"
//};

//using(var context=new KeysContext())
//{
//    context.Keys.Add(dp1);
//    context.Keys.Add(dp2);

//    context.Keys.Add(q1);
//    context.Keys.Add(q2);
//    context.Keys.Add(q3);

//    context.SaveChanges();
//}

//List<DsaDomainParameter> dpList = new List<DsaDomainParameter>();
//List<DsaPrivateKey> keyList = new List<DsaPrivateKey>();

//using (var db=new KeysContext())
//{
//    foreach(var el in db.DsaPrivateKeys)
//    {
//        keyList.Add(el);
//    }

//    var zhopa = keyList[0].DomainParameter;

//    //var w = keyList[0].KeyType.GetType();
//    Console.WriteLine(zhopa.GetType());
//    //Console.WriteLine(w == typeof(string));
//    //Console.WriteLine(w == typeof(KeyType));
//    //Console.WriteLine(keyList[0].KeyType);
//    //Console.WriteLine(keyList[0].KeyType.GetType());
//    //foreach (var item in db.DsaDomainParameters)
//    //{
//    //    dpList.Add(item);
//    //}
//}

//PrimalityVerificator w = new MillerRabinPrimalityVerificator();
//NumberGenerator q = new FibonacciNumberGenerator();

//int s = 0;
//for (int i = 1; i < 100; i++)
{
    //if(i%1000==0)
    //  Console.WriteLine($"\t{i}");
    //BigInteger num = i;

    //var s = num.GetBitLength();
    // var d = num.GetBinaryLength();

    // Console.WriteLine($"{s}  {d}");

    //BigInteger n = i;

    //Console.WriteLine($"{n}:{w.IsPrime(n)}");

    //BigInteger min = 1274617846;
    //BigInteger max = 12746178462355;
    //BigInteger num = q.GenerateNumber(min, max);

    //if (num <= min+10 || num >= max-10)
    //    Console.WriteLine("PEZDEC");
    //else
    //    s++;

    //BigInteger num = i;

    //if (w.IsPrime(num))
    //    Console.WriteLine(num);
}

//byte[] data = new byte[54];

//for (int i = 0; i < data.Length; i++)
//{
//    data[i] = 255;
//}

//var q = HashAlgorithm.Preprocess(data, 64, 1, 8);

//for (int i = 0; i < q.Length; i++)
//{
//    Console.WriteLine(Convert.ToString(q[i], 2).PadLeft(8, '0'));
//}

//string str = "zhopa";
//HashAlgorithm q = new MD_5();
////while (true)
//{
//    //str = Console.ReadLine();

//    var bytes = Encoding.UTF8.GetBytes(str);

//    var has = q.GetHash(bytes);

//    string hh = "";

//    for (int i = 0; i < has.Length; i++)
//    {
//        hh += Convert.ToString(has[i], 16);
//    }

//    Console.WriteLine(hh);
//}
//int n = 21;

//byte[] arr = new byte[n];
//int s = BlockConverter.GetBlockSize(BigInteger.Pow(256, 5) + 10000);

//Console.WriteLine(s);

//for (int i = 0; i < n; i++)
//{
//    arr[i] = Convert.ToByte((i * i) % 256);
//    Console.WriteLine(Convert.ToString(arr[i],2).PadLeft(8, '0'));
//}
//Console.WriteLine();
//var blocks = BlockConverter.BytesToBlocks(arr, s);

//for (int i = 0; i < blocks.Length; i++)
//{
//    Console.WriteLine(blocks[i].ToBinaryString());
//}
//Console.WriteLine();

//List<byte> lis = new List<byte>();

//for (int i = 0; i < blocks.Length; i++)
//{
//    var bl = BlockConverter.BlockToBytes(blocks[i],s);

//    for (int j = 0; j < bl.Length; j++)
//    {
//        lis.Add(bl[j]);
//    }
//}

//lis.RemoveAll(el => el == 0);

//foreach (var el in lis)
//{
//    Console.WriteLine(Convert.ToString(el, 2).PadLeft(8, '0'));
//}

Random rand=new Random();

const int rsaKeysBinarySize = 64;

int iterationsCount = 1000;
int errorsCount = 0;

for (int i = 0; i < iterationsCount; i++)
{
    if (i % (iterationsCount/100) == 0)
        Console.WriteLine(i);

    bool good = true;

    // генерация текста
    //-------------------------
    StringBuilder text = new StringBuilder();

    int messageLength = rand.Next(1024);

    for (int j = 0; j < messageLength; j++)
    {
        text.Append(Convert.ToChar(rand.Next() % 256));
    }

    string message = text.ToString();
    message = "zhopa";
    var messageBytes = Encoding.Unicode.GetBytes(message);
    // --------------------

    // генераторы
    // --------------------
    PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();
    NumberGenerator numberGenerator = new FibonacciNumberGenerator(primalityVerificator);
    HashAlgorithm hashAlgorithm = new SHA_256();
    // --------------------

    // генерация ключей
    // --------------------
    AsymmetricKey rsaPrivateKey;
    AsymmetricKey rsaPublicKey;

    RsaKeysGenerator rsaKeysGenerator = new RsaKeysGenerator(numberGenerator, primalityVerificator, hashAlgorithm);
    rsaKeysGenerator.GenerateKeys(rsaKeysBinarySize, out rsaPrivateKey, out rsaPublicKey);
    RSA rSA = new RSA(numberGenerator, primalityVerificator, hashAlgorithm);

    AsymmetricKey elGamalPrivateKey;
    AsymmetricKey elGamalPublicKey;

    ElGamalKeysGenerator elGamalKeysGenerator = new ElGamalKeysGenerator(numberGenerator, primalityVerificator, hashAlgorithm);
    elGamalKeysGenerator.GenerateKeys(16, out elGamalPrivateKey, out elGamalPublicKey);
    ElGamal elGamal = new ElGamal(numberGenerator, primalityVerificator, hashAlgorithm);

    AsymmetricKey dsaPrivateKey;
    AsymmetricKey dsaPublicKey;

    DsaKeysGenerator dsaKeysGenerator = new DsaKeysGenerator(numberGenerator, primalityVerificator, hashAlgorithm);
    dsaKeysGenerator.GenerateKeys(384, out dsaPrivateKey, out dsaPublicKey);
    DSA dSA = new DSA(numberGenerator, primalityVerificator, hashAlgorithm);
    // --------------------

    // encryption
    // --------------------

    var rsaEncryption = rSA.Encrypt(messageBytes, rsaPublicKey);
    var rsaDecryption = rSA.Decrypt(rsaEncryption, rsaPrivateKey);

    var rsaDecryptedText = Encoding.Unicode.GetString(rsaDecryption);

    if (message != rsaDecryptedText)
    {
        good = false;
        Console.WriteLine("RSA encryption error");
    }

    var elGamalEncryption = elGamal.Encrypt(messageBytes, elGamalPublicKey);
    var elGamalDecryption = elGamal.Decrypt(elGamalEncryption, elGamalPrivateKey);

    var elGamalDecryptedText = Encoding.Unicode.GetString(elGamalDecryption);

    if (message != elGamalDecryptedText)
    {
        good = false;
        Console.WriteLine("ElGamal encryption error");
    }
    // --------------------

    // signatutaturing
    // --------------------

    var rsaSign = rSA.CreateSignature(messageBytes, rsaPrivateKey);
    if (rSA.VerifyDigitalSignature(rsaSign, messageBytes, rsaPublicKey) == false)
    {
        good = false;
        Console.WriteLine("RSA sign error");
    }

    var elGamalSign = elGamal.CreateSignature(messageBytes, elGamalPrivateKey);
    if (elGamal.VerifyDigitalSignature(elGamalSign, messageBytes, elGamalPublicKey) == false)
    {
        good = false;
        Console.WriteLine("ElGamal sign error");
    }

    //var dsaSign = dSA.CreateSignature(messageBytes, dsaPrivateKey);
    //if (dSA.VerifyDigitalSignature(dsaSign, messageBytes, dsaPublicKey) == false)
    {
      //  good = false;
        //Console.WriteLine("DSA sign error");
    }
    // --------------------

    if (good == false)
        errorsCount++;
}

double res = 100 - (errorsCount * 100 / iterationsCount);

Console.WriteLine($"Errors:{errorsCount}");
Console.WriteLine($"Result:{res}%");