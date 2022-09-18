using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;
using UrunSatisSitesi.WebUI.Utils;

namespace UrunSatisSitesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PostsController : Controller
    {
        private readonly IRepository<Post> _repository;

        public PostsController(IRepository<Post> repository)
        {
            _repository = repository;
        }

        // GET: PostsController
        public async Task<IActionResult> Index()
        {
            var liste = await _repository.GetAllAsync();
            return View(liste);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Post post, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) post.Image = await FileHelper.FileLoaderAsync(Image);
                    await _repository.AddAsync(post);
                    await _repository.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(post);
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _repository.Find(id);

            return View(data);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Post post, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) post.Image = await FileHelper.FileLoaderAsync(Image);
                    _repository.Update(post);
                    await _repository.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(post);
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _repository.Find(id);

            return View(data);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _repository.Delete(post);
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
