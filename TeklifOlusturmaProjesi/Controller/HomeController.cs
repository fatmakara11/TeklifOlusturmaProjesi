

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeklifOlusturmaProjesi.Models;

using ORM.DB.Repository;
using ORM.Models.Models;

namespace TeklifOlusturmaProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMusteriRepository _musteriRepository;
        private readonly IAracıKurumRepository _aracıKurumRepository;
        private readonly IUrunRepository _urunRepository;
        private readonly ITeklifRepository _teklifRepository;


        public HomeController(ILogger<HomeController> logger, IMusteriRepository musteriRepository, IAracıKurumRepository aracıKurumRepository, IUrunRepository urunRepository, ITeklifRepository teklifRepository)
        {
            _logger = logger;
            _musteriRepository = musteriRepository;
            _aracıKurumRepository = aracıKurumRepository;
            _urunRepository = urunRepository;
            _teklifRepository = teklifRepository;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var musteriler = await _musteriRepository.GetAllAsync();
            var aracıKurumlar = await _aracıKurumRepository.GetAllAsync(); // AracıKurum verisini alın
            var urunler = await _urunRepository.GetAllAsync();
            var teklifler = await _teklifRepository.GetAllTekliflerAsync();
            ViewBag.Urun = urunler;
            ViewBag.Musteri = musteriler;
            ViewBag.AracıKurum = aracıKurumlar; // ViewBag'e ekleyin
            ViewBag.Teklif = teklifler;
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(Teklif teklif)
        {



            // TeklifID ve zaman bilgilerini oluşturma
            teklif.TeklifID = Guid.NewGuid();
            teklif.OlusturmaZamanı = DateTime.Now;
            teklif.GüncellemeZamanı = DateTime.Now;

            // Veritabanı işlemleri için repository'leri kullanarak ilgili verileri çekme
            var musteriler = await _musteriRepository.GetAllAsync();
            var aracıKurumlar = await _aracıKurumRepository.GetAllAsync();
            var urunler = await _urunRepository.GetAllAsync();

            // ViewBag'e gerekli verileri ekleyin
            ViewBag.Urun = urunler;
            ViewBag.Musteri = musteriler;
            ViewBag.AracıKurum = aracıKurumlar;

            // Teklifi veritabanına ekleyin
            bool result = _teklifRepository.AddTeklif(teklif);
            if (result)
            {
                FilterReportModel filter = new FilterReportModel();
                filter.MusteriID = teklif.MusteriID;
                string w = $" WHERE {nameof(Teklif.MusteriID)} = '{filter.MusteriID}'";
                // Fetch data from the repository using the custom query
                var ret = _teklifRepository.GetAllByCustomQuery(w).ToList();

                return PartialView("_PartialTeklifOlusturma", ret);
            } 

            // Tüm teklifleri yeniden veritabanından çekin ve tabloyu güncellemek için ViewBag'e atayın
            var teklifler = await _teklifRepository.GetAllTekliflerAsync();
            ViewBag.Teklifler = teklifler;

            // View'ı geri döndürün partial 
            return View(teklifler);


        }
        [HttpPost]
        public IActionResult FilterTeklifler(FilterReportModel filter)
        {
            List<Teklif> ret = new List<Teklif>();

            try
            {
                if (filter != null)
                {
                    // Create a custom query based on the filter inputs
                    //bool result = _teklifRepository.AddTeklif(filter);
                    string w = $" WHERE {nameof(Teklif.MusteriID)} = '{filter.MusteriID}'";
                    // Fetch data from the repository using the custom query
                    ret = _teklifRepository.GetAllByCustomQuery(w).ToList();
                }

                // Return the filtered results as a Partial View
                return PartialView("_PartialTeklifOlusturma", ret);
            }
            catch (Exception ex)
            {
                // Error handling - logging or returning an error message
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult PartialTeklifOlusturma()
        {
            return PartialView("_PartialTeklifOlusturma");
        }

        [HttpGet]
        public async Task<IActionResult> GetMusteri()
        {
            var Musteri = await _musteriRepository.GetAllAsync();
            return Json(Musteri);
        }

        [HttpPost]
        public async Task<IActionResult> AddMusteri([FromBody] Musteri musteri)
        {
            if (musteri == null || string.IsNullOrEmpty(musteri.MusteriAdı))
            {
                return BadRequest("Geçersiz müşteri bilgisi.");
            }

            await _musteriRepository.AddMusteri(musteri);
            return Ok(new { Success = true });
        }
        [HttpGet("aracıKurum")]
        public async Task<IActionResult> GetAracıKurum()
        {
            var AracıKurum = await _aracıKurumRepository.GetAllAsync();
            return Json(AracıKurum);
        }

        [HttpPost("aracıKurum")]
        public async Task<IActionResult> AddAracıKurum([FromBody] AracıKurum aracıKurum)
        {
            if (aracıKurum == null || string.IsNullOrEmpty(aracıKurum.AracıKurumAdı))
            {
                return BadRequest("Geçersiz aracı kurum bilgisi.");
            }

            await _aracıKurumRepository.AddAracıKurum(aracıKurum);
            return Ok(new { Success = true });
        }
        public async Task<ActionResult> Add(string element, string newItem)
        {
            try
            {
                if (element == "musteri")
                {
                    Musteri musteri = new Musteri
                    {
                        MusteriID = Guid.NewGuid(),
                        MusteriAdı = newItem,
                        OlusturmaZamanı = DateTime.Now,
                        GüncellemeZamanı = DateTime.Now,
                        Aktif = true,
                        Pasif = false
                    };
                    await _musteriRepository.AddMusteri(musteri);
                    return Json(true);
                }
                else if (element == "aracı_kurum")
                {
                    AracıKurum aracıKurum = new AracıKurum
                    {
                        AracıKurumID = Guid.NewGuid(),
                        AracıKurumAdı = newItem,
                        OlusturmaZamanı = DateTime.Now,
                        GüncellemeZamanı = DateTime.Now,
                        Aktif = true,
                        Pasif = false
                    };
                    await _aracıKurumRepository.AddAracıKurum(aracıKurum);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item");
                return Json(false);
            }
        }

        [HttpGet("Home/GetUrunDetay/{ÜrünID}")]
        public async Task<IActionResult> GetUrunDetay(Guid ÜrünID)
        {
            var urun = await _urunRepository.GetById(ÜrünID);
            if (urun == null)
            {
                return NotFound();
            }

            var urunDetay = new
            {
                birim = urun.Birim,
                birimFiyat = urun.BirimFiyat,
                stokAdeti = urun.StokAdeti
            };

            return Json(urunDetay);
        }
    }

}
