using Microsoft.Data.SqlClient;  // SqlConnection sınıfını kullanmak için gerekli.
using Microsoft.Extensions.Configuration;
using System.Data;  // IDbConnection arayüzünü kullanmak için gerekli.

namespace ORM.DB.Data  // Data katmanını belirtir.
{
    // IDapperContext arayüzünden türeyen DapperContext sınıfı.
    // Bu sınıf, Dapper ile veritabanı bağlantıları oluşturmak için kullanılır.
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;  // IConfiguration arayüzü, yapılandırma ayarlarını okumak için kullanılır.
        private readonly string _connString;  // Veritabanı bağlantı dizesini saklar.

        // Yapıcı metod (constructor), DapperContext sınıfının bir örneği oluşturulduğunda çalıştırılır.
        // IConfiguration nesnesini ve bağlantı dizesini alır.
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;  // Parametre olarak alınan configuration nesnesi, sınıfın _configuration alanına atanır.
            _connString = _configuration.GetConnectionString("ConStr");  // configuration nesnesi kullanılarak appsettings.json'dan bağlantı dizesi okunur.
        }

        // CreateConnection metodu, yeni bir SqlConnection nesnesi oluşturur ve döndürür.
        // Bu metod, veritabanı bağlantıları oluşturmak için kullanılır.
        public IDbConnection CreateConnection() => new SqlConnection(_connString);
    }
}
