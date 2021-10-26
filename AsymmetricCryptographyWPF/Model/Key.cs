using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using AsymmetricCryptography;

namespace AsymmetricCryptographyWPF.Model
{
    class Key
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlgorithmName { get; set; }
        public string Permission { get; set; }
        public int BinarySize { get; set; }
        public List<RsaPrivateKey> RsaPrivateKeys { get; set; }
        public List<RsaPublicKey> RsaPublicKeys { get; set; }
    }
}
