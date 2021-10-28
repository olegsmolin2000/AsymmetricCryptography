using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AsymmetrycCryptographyDataLayer.Entities.Keys
{
    public class AsymmetricKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlgorithmName { get; set; }
        public string Permittion { get; set; }

        //public List<RsaPrivateKey> RsaPrivateKeys { get; set; }
        //public List<RsaPublicKey> RsaPublicKeys { get; set; }
    }
}
