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
    public interface  IAracıKurumRepository : IGenericRepository<AracıKurum>
    {
        Task<IEnumerable<AracıKurum>> GetAllAsync();
        Task AddAracıKurum(AracıKurum aracıKurum);
    }
    public class AracıKurumRepository : GenericRepository<AracıKurum>, IAracıKurumRepository
    {
        private readonly IDapperContext _context;
        public AracıKurumRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AracıKurum>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<AracıKurum>("SELECT * FROM AracıKurum");
            }
        }

        public async Task AddAracıKurum(AracıKurum aracıKurum)
        {
            var query = "insert into AracıKurum (AracıKurumAdı) values (@AracıKurumAdı)";
            var parameters = new DynamicParameters();
            parameters.Add("@AracıKurumAdı", aracıKurum.AracıKurumAdı);



            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }




    }

}
