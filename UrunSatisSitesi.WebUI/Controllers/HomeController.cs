using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;
using UrunSatisSitesi.WebUI.Models;

namespace UrunSatisSitesi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Slider> _slider;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Contact> _contactRepository;

        public HomeController(IRepository<Slider> slider, IRepository<Product> productRepository, IRepository<Contact> contactRepository)
        {
            _slider = slider;
            _productRepository = productRepository;
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            HomePageViewModel viewModel = new();

            var sliders = await _slider.GetAllAsync();

            var products = await _productRepository.GetAllAsync(p => p.IsActive && p.IsHome); // Ürünlerden sadece aktif olarak ve anasayfa ürünü olarak işaretlenmiş olanları getir.

            viewModel.Sliders = sliders;
            viewModel.Products = products;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepository.AddAsync(contact);
                await _contactRepository.SaveChangesAsync();
                TempData["mesaj"] = "<div class='alert alert-success'>Mesajınız Gönderilmiştir. Teşekkürler..</div>";
                return RedirectToAction("Contact");
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}