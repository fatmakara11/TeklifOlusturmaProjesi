using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ORM.Models.Models
{
    public class Musteri:MusteriDetay
    {
        public Guid MusteriID { get; set; }
        public DateTime OlusturmaZamanı { get; set; }
        public DateTime GüncellemeZamanı { get; set; }
        public bool Aktif { get; set; }
        public bool Pasif { get; set; }
     
    }
}
