using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DB.Repository;
using ORM.Models.Models;



namespace ORM.Business.Service
{



        public interface IUrunService
        {
            Task<IEnumerable<Urun>> GetAllUrun();
            Task<Urun> GetById(Guid ÜrünID);
            Task AddUrun(Urun urun);
            Task UpdateStok(Guid ÜrünID, int StokAdeti);
        }

        public class UrunService : IUrunService
        {
            private readonly IUrunRepository _repository;

            public UrunService(IUrunRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<Urun>> GetAllUrun()
            {
                return await _repository.GetAllAsync();
            }

            public async Task<Urun> GetById(Guid ÜrünID)
            {
                return await _repository.GetById(ÜrünID);
            }

            public async Task AddUrun(Urun urun)
            {
                await _repository.AddUrun(urun);
            }

            public async Task UpdateStok(Guid ÜrünID, int StokAdeti)
            {
                await _repository.UpdateStok(ÜrünID, StokAdeti);
            }
        }
    

}
