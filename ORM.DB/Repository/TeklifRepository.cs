using Dapper;
using ORM.DB.Data;
using ORM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DB.Repository
{
    public interface ITeklifRepository :IGenericRepository<Teklif>
    {
        Task<IEnumerable<Teklif>> GetAllTekliflerAsync();
        Task<Teklif> GetTeklifById(Guid TeklifID);
        bool AddTeklif(Teklif teklif);
        Task UpdateTeklif(Teklif teklif);
        Task  DeleteTeklif(Guid TeklifID);
        IEnumerable<Teklif> GetAllByCustomQuery(string queryCondition);
    }
    public class TeklifRepository : GenericRepository<Teklif>, ITeklifRepository
    {
        private readonly IDapperContext _context;
        public TeklifRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public bool AddTeklif(Teklif teklif)
        {

            using (var connection = _context.CreateConnection())
            {
                var query = @"INSERT INTO Teklif (TeklifID, MusteriID, ÜrünID, AracıKurumID, Adet, Birim, BirimFiyat, ToplamFiyat, OlusturmaZamanı, GüncellemeZamanı, Aktif, Pasif)  VALUES (@TeklifID, @MusteriID, @ÜrünID, @AracıKurumID, @Adet, @Birim, @BirimFiyat, @ToplamFiyat, @OlusturmaZamanı, @GüncellemeZamanı, @Aktif, @Pasif)";


                var parameters = new DynamicParameters();
                parameters.Add("@TeklifID", teklif.TeklifID);
                parameters.Add("@MusteriID", teklif.MusteriID);
                parameters.Add("@ÜrünID", teklif.ÜrünID);
                parameters.Add("@AracıKurumID", teklif.AracıKurumID);
                parameters.Add("@Adet", teklif.Adet);
                parameters.Add("@Birim", teklif.Birim);
                parameters.Add("@BirimFiyat", teklif.BirimFiyat);
                parameters.Add("@ToplamFiyat", teklif.ToplamFiyat);
                parameters.Add("@OlusturmaZamanı", teklif.OlusturmaZamanı);
                parameters.Add("@GüncellemeZamanı", teklif.GüncellemeZamanı);
                parameters.Add("@Aktif", teklif.Aktif);
                parameters.Add("@Pasif", teklif.Pasif);

                int row = connection.Execute(query, parameters);
                if (row > 0)
                {
                    return true;
                }
                else { return false; }

            }
        }

        public Task DeleteTeklif(Guid TeklifID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Teklif> GetAllByCustomQuery(string queryCondition)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = $"SELECT * FROM Teklif {queryCondition}";
                return connection.Query<Teklif>(query).ToList();
            }
        }
        public async Task<IEnumerable<Teklif>> GetAllTekliflerAsync()
        {
           
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "SELECT * FROM Teklif WHERE Aktif = @Aktif";
                    var parameters = new { Aktif = true }; 

                    return await connection.QueryAsync<Teklif>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi, örneğin loglama
                throw new ApplicationException("Veriler alınırken bir hata oluştu.", ex);
            }

         }

        public async Task<Teklif> GetTeklifById(Guid TeklifID)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Teklif WHERE TeklifID = @TeklifID";

                var parameters = new DynamicParameters();
                parameters.Add("@TeklifID", TeklifID);

                return await connection.QuerySingleOrDefaultAsync<Teklif>(query, parameters);
            }
        }

        public Task  UpdateTeklif(Teklif teklif)
        {
            throw new NotImplementedException();
        }

     
    }
}
