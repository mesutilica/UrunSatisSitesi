using Microsoft.AspNetCore.Mvc;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;

namespace UrunSatisSitesi.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category> _repositoryCategory;

        public CategoriesController(IRepository<Category> repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }

        public IActionResult Index(int id)
        {
            var kategori = _repositoryCategory.Find(id);

            return View(kategori);
        }
    }
}
