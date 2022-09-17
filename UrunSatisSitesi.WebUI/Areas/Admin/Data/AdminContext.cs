using Microsoft.EntityFrameworkCore;
using UrunSatisSitesi.Entities;

namespace UrunSatisSitesi.WebUI.Areas.Admin.Data
{
    public class AdminContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Burada optionsBuilder ı kullanarak sql server ayarlarımızı belirleyebiliyoruz
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=UrunSatisSitesi; trusted_connection=true;"); // bu metot ile uygulamada sql server kullanacağımızı belirttik
            base.OnConfiguring(optionsBuilder);
        }

    }
}
