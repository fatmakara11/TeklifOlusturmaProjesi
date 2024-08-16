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
    public interface IMusteriRepository : IGenericRepository<Musteri>
    {
        Task<IEnumerable<Musteri>> GetAllAsync();
        Task AddMusteri(Musteri musteri);
    }

    // MusteriRepository sınıfı
    public class MusteriRepository : GenericRepository<Musteri>, IMusteriRepository
    {
        private readonly IDapperContext _context;

        public MusteriRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Musteri>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Musteri>("SELECT * FROM Musteri");
            }
        }

        public async Task AddMusteri(Musteri musteri)
        {
            var query = "İNSERT İNTO  Musteri (MusteriAdı) values (@MusteriAdı)";
            var parameters = new DynamicParameters();
            parameters.Add("@MusteriAdı", musteri.MusteriAdı);



            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }




    }
}
