using UrunSatisSitesi.Data;
using UrunSatisSitesi.Service.Repositories;
using UrunSatisSitesi.WebUI.Areas.Admin.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // Oturum iþlemleri için gerekli kütüphane

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<AdminContext>();
builder.Services.AddDbContext<DatabaseContext>(); //options => options.UseSqlServer() uygulamada dabasecontext imizde sql server kullanacaðýmýzý bildirdik
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); // Projede bir yerde IRepository interfaci kullanýlmak istenirse, Repository nesnesinden bir örnek oluþtur ve kullanýma sun.
// Dependency injection yöntemi olarak 3 farklý yöntemimiz var;
// 1-AddSingleton : Oluþturmasý istenen nesneden uygulama çalýþtýðýnda 1 tane oluþtururu ve her istekte bu nesneyi gönderir.
// 2-AddTransient : Oluþturmasý istenen nesne için gelen her istekte yani bir nesne oluþturur.
// 3-AddScoped : Oluþturmasý istenen nesne için gelen isteðe bakarak eðer daha önce oluþmuþ bir örnek varsa geriye onu döner, yoksa yeni nesne oluþturup döner.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "Admin";
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // Cookie süresi 1 gün belirledik
    x.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication(); // oturum açma iþlemi yapacaðýz admin için
app.UseAuthorization(); // Authorization : yetkilendirme, rol vb iþlemler için

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
