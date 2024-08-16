using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models.Models
{
    public class Teklif
    {
        public Guid TeklifID { get; set; }
        public Guid MusteriID { get; set; }
        public Guid ÜrünID { get; set; }
        public Guid AracıKurumID { get; set; }
        public int Adet { get; set; }
        public string? Birim { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
        public DateTime OlusturmaZamanı { get; set; }
        public DateTime GüncellemeZamanı { get; set; }
        public bool Aktif { get; set; }
        public bool Pasif { get; set; }

    }
}
