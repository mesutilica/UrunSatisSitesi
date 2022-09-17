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

        public HomeController(IRepository<Slider> slider)
        {
            _slider = slider;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var sliders = await _slider.GetAllAsync();

            return View(sliders);
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