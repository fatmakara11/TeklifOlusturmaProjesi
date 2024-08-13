using Dapper;
using Microsoft.Data.SqlClient;
using ORM.DB.Data;
using ORM.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM.DB.Repository
{
    public interface IUrunRepository : IGenericRepository<Urun>
    {
        Task<IEnumerable<Urun>> GetAllAsync();
        Task<Urun> GetById(Guid ÜrünID);
        Task AddUrun(Urun urun);
        Task UpdateStok(Guid ÜrünID, int Adet);
    }

    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {
        private readonly IDapperContext _context;

        public UrunRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Urun>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Urun WHERE Aktif = 1";
                return await connection.QueryAsync<Urun>(query);
            }
        }

        public async Task<Urun> GetById(Guid ÜrünID)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Urun WHERE ÜrünID = @ÜrünID";

                var parameters = new DynamicParameters();
                parameters.Add("@ÜrünID", ÜrünID);

                return await connection.QuerySingleOrDefaultAsync<Urun>(query, parameters);
            }
        }

        public async Task AddUrun(Urun urun)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "INSERT INTO Urun (ÜrünID, ÜrünAdı, Birim, BirimFiyat, StokAdeti, OlusturmaZamanı, GüncellemeZamanı, Aktif, Pasif) VALUES (@ÜrünID, @ÜrünAdı, @Birim, @BirimFiyat, @StokAdeti, @OlusturmaZamanı, @GüncellemeZamanı, @Aktif, @Pasif)";



                var parameters = new DynamicParameters();
                parameters.Add("@ÜrünID", urun.ÜrünID);
                parameters.Add("@ÜrünAdı", urun.ÜrünAdı);
                parameters.Add("@Birim", urun.Birim);
                parameters.Add("@BirimFiyat", urun.BirimFiyat);
                parameters.Add("@StokAdeti", urun.StokAdeti);
                parameters.Add("@OlusturmaZamanı", urun.OlusturmaZamanı);
                parameters.Add("@GüncellemeZamanı", urun.GüncellemeZamanı);
                parameters.Add("@Aktif", urun.Aktif);
                parameters.Add("@Pasif", urun.Pasif);

                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateStok(Guid ÜrünID, int adet)
        {
            using (var connection = _context.CreateConnection())
            {
                //var query = "UPDATE Urun SET StokAdeti = StokAdeti - @Adet WHERE ÜrünID = @ÜrünID";
                var query = "UPDATE Urun SET Adet= @Adet WHERE ÜrünID = @ÜrünID";

                var parameters = new DynamicParameters();
                parameters.Add("@ÜrünID", ÜrünID);
                parameters.Add("@Adet", adet);

                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
