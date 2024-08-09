using ORM.DB.Repository;
using ORM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Business.Service
{
    public interface IMusteriService
{
    Task<IEnumerable<Musteri>> GetAllMusteri();
    Task AddMusteri(Musteri musteri);
}

public class MusteriService : IMusteriService
{
    private readonly IMusteriRepository _repository;

    public MusteriService(IMusteriRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Musteri>> GetAllMusteri()
    {
        return await _repository.GetAllAsync();
    }

    public async Task AddMusteri(Musteri musteri)
    {
        await _repository.AddMusteri(musteri);
        

    }

 
    }
}
