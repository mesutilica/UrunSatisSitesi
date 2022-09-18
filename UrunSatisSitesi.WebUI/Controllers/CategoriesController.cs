using Microsoft.AspNetCore.Mvc;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Abstract;
using UrunSatisSitesi.Service.Repositories;

namespace UrunSatisSitesi.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly IRepository<Category> _repositoryCategory;

        private readonly ICategoryRepository _categoryRepository;

        /*
         An unhandled exception occurred while processing the request.

InvalidOperationException: Unable to resolve service for type 'UrunSatisSitesi.Service.Abstract.ICategoryRepository' while attempting to activate 'UrunSatisSitesi.WebUI.Controllers.CategoriesController'.

        Dikkat! Yukardaki hatayı aldığımızda kullanmak istediğimiz interface program.cs de servis olarak eklenmemiş demektir!!
        Oraya add services diyerek eklersek sorun çözülür..
         */

        public CategoriesController(IRepository<Category> repositoryCategory, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            //_repositoryCategory = repositoryCategory;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            //var kategori = _repositoryCategory.Find(id);

            var kategori = await _categoryRepository.KategoriyiUrunlerleBirlikteGetir(id);

            return View(kategori);
        }
    }
}
