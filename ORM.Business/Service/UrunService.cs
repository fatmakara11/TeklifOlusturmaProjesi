﻿using System;
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
            Task<Urun> GetById(Guid urunID);
            Task AddUrun(Urun urun);
            Task UpdateStok(Guid urunID, int adet);
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

            public async Task<Urun> GetById(Guid urunID)
            {
                return await _repository.GetById(urunID);
            }

            public async Task AddUrun(Urun urun)
            {
                await _repository.AddUrun(urun);
            }

            public async Task UpdateStok(Guid urunID, int adet)
            {
                await _repository.UpdateStok(urunID, adet);
            }
        }
    

}
