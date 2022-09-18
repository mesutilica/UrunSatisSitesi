using Microsoft.EntityFrameworkCore;
using UrunSatisSitesi.Data;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Abstract;
using UrunSatisSitesi.Service.Repositories;

namespace UrunSatisSitesi.Service.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Category> KategoriyiUrunlerleBirlikteGetir(int categoryId)
        {
            return await _databaseContext.Categories.Include(p => p.Products).FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
