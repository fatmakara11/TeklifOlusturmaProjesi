using ORM.DB.Repository;
using ORM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Business.Service
{
    public interface ITeklifService
    {
        Task<IEnumerable<Teklif>> GetAllTekliflerAsync();
        Task<Teklif> GetTeklifById(Guid TeklifID);
        bool AddTeklif(Teklif teklif);
        //Task AddTeklif(Teklif teklif);
        Task UpdateTeklif(Teklif teklif);
        Task DeleteTeklif(Guid TeklifID);

    }
    public class TeklifService : ITeklifService
    {

        private readonly ITeklifRepository _repository;

        public TeklifService(ITeklifRepository repository)
        {
            _repository = repository;
        }

        public  bool AddTeklif(Teklif teklif)
        {
            return  _repository.AddTeklif(teklif);
        }

        public Task DeleteTeklif(Guid TeklifID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Teklif>> GetAllTekliflerAsync()
        {
            return await _repository.GetAllTekliflerAsync();
        }

        public  async Task<Teklif> GetTeklifById(Guid TeklifID)
        {
            return await _repository.GetTeklifById(TeklifID);
        }

        public async Task UpdateTeklif(Teklif teklif)
        {
            await _repository.UpdateTeklif(teklif);
        }

        //async Task ITeklifService.AddTeklif(Teklif teklif)
        //{
        //    await _repository.AddTeklif(teklif);
        //}
    }
}
