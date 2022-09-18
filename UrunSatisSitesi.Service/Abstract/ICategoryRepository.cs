using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;

namespace UrunSatisSitesi.Service.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> KategoriyiUrunlerleBirlikteGetir(int categoryId);
    }
}
