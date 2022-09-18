using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;
using UrunSatisSitesi.WebUI.Utils;

namespace UrunSatisSitesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _repository;

        public NewsController(IRepository<News> newsRepository)
        {
            _repository = newsRepository;
        }

        // GET: NewsController
        public async Task<IActionResult> Index()
        {
            var liste = await _repository.GetAllAsync();
            return View(liste);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(News news, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) news.Image = await FileHelper.FileLoaderAsync(Image);
                    await _repository.AddAsync(news);
                    await _repository.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(news);
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _repository.Find(id);

            return View(data);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, News news, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) news.Image = await FileHelper.FileLoaderAsync(Image);
                    _repository.Update(news);
                    await _repository.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(news);
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _repository.Find(id);

            return View(data);
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, News news)
        {
            try
            {
                _repository.Delete(news);
                _repository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
