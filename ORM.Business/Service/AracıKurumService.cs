using ORM.DB.Repository;
using ORM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Business.Service
{
    public interface IAracıKurumService
    {
        Task<IEnumerable<AracıKurum>> GetAllAracıKurum();
        Task AddAracıKurum(AracıKurum aracıKurum);
    }

    public class AracıKurumService : IAracıKurumService
    {
        private readonly IAracıKurumRepository _repository;

        public AracıKurumService(IAracıKurumRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AracıKurum>> GetAllAracıKurum()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAracıKurum(AracıKurum aracıKurum)
        {
            await _repository.AddAracıKurum(aracıKurum);


        }

   

 
    }
}
