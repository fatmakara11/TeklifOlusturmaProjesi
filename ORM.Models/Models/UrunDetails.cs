using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models.Models
{
    public class UrunDetails
    {
        public string? ÜrünAdı { get; set; }
        public int StokAdeti { get; set; }
        public decimal BirimFiyat { get; set; }
        public string? Birim { get; set; }

    }
}
