
using ORM.DB.Data;
using ORM.DB.Repository;

var builder = WebApplication.CreateBuilder(args);

// Servisleri konteyn�ra ekleyin.
builder.Services.AddControllersWithViews();

// DapperContext'i DI konteynerine ekleyin
builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddScoped<IMusteriRepository, MusteriRepository>();
builder.Services.AddScoped<IArac�KurumRepository, Arac�KurumRepository>();
builder.Services.AddScoped<IUrunRepository, UrunRepository>();


// Generic repository'yi scoped olarak ekleyin
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// HTTP istek i�leme boru hatt�n� yap�land�r�n.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
