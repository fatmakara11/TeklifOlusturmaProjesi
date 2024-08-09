using System.Collections.Generic;

namespace ORM.DB.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string tableName);
        Task<T> GetById(string tableName, Guid id);
        Task Delete(string tableName, Guid id);
        Task Add(string tableName, T entity);
        Task Update(string tableName, T entity);
     
    }
}