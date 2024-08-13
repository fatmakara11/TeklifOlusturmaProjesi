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
        public HomeController(ILogger<HomeController> logger, IMusteriRepository musteriRepository, IAracıKurumRepository aracıKurumRepository, IUrunRepository urunRepository)
        {
            _logger = logger;
            _musteriRepository = musteriRepository;
            _aracıKurumRepository = aracıKurumRepository;
            _urunRepository = urunRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var musteriler = await _musteriRepository.GetAllAsync();
            var aracıKurumlar = await _aracıKurumRepository.GetAllAsync(); // AracıKurum verisini alın
            var urunler = await _urunRepository.GetAllAsync();
            ViewBag.Urun = urunler;
            ViewBag.Musteri = musteriler;
            ViewBag.AracıKurum = aracıKurumlar; // ViewBag'e ekleyin
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Teklif teklif)
        {
            var musteriler = await _musteriRepository.GetAllAsync();
            var aracıKurumlar = await _aracıKurumRepository.GetAllAsync(); // AracıKurum verisini alın
            var urunler = await _urunRepository.GetAllAsync();
            ViewBag.Urun = urunler;
            ViewBag.Musteri = musteriler;
            ViewBag.AracıKurum = aracıKurumlar;
            return View();
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
