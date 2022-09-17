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

        public HomeController(IRepository<Slider> slider, IRepository<Product> productRepository)
        {
            _slider = slider;
            _productRepository = productRepository;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}