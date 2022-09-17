using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;
using UrunSatisSitesi.WebUI.Utils;

namespace UrunSatisSitesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Category> _repositoryCategory;
        private readonly IRepository<Brand> _repositoryBrand;

        public ProductsController(IRepository<Product> repository, IRepository<Category> repositoryCategory, IRepository<Brand> repositoryBrand)
        {
            _repository = repository;
            _repositoryCategory = repositoryCategory;
            _repositoryBrand = repositoryBrand;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            var liste = await _repository.GetAllAsync();
            return View(liste);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var liste = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(liste, "Id", "Name");

            var markaliste = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markaliste, "Id", "Name");

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product product, IFormFile? Image)
        {
            try
            {
                if (Image is not null) product.Image = await FileHelper.FileLoaderAsync(Image);
                await _repository.AddAsync(product);
                await _repository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }

            var liste = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(liste, "Id", "Name");

            var markaliste = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markaliste, "Id", "Name");

            return View(product);
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var kayit = await _repository.FindAsync(id);

            var liste = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(liste, "Id", "Name");

            var markaliste = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markaliste, "Id", "Name");

            return View(kayit);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product product, IFormFile? Image)
        {
            try
            {
                if (Image is not null) product.Image = await FileHelper.FileLoaderAsync(Image);
                _repository.Update(product);
                await _repository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }

            var liste = await _repositoryCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(liste, "Id", "Name");

            var markaliste = await _repositoryBrand.GetAllAsync();
            ViewBag.BrandId = new SelectList(markaliste, "Id", "Name");

            return View(product);
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var kayit = await _repository.FindAsync(id);

            if (kayit != null)
                _repository.Delete(kayit);

            return RedirectToAction(nameof(Index));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                _repository.Delete(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
