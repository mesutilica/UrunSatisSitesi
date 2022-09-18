using Microsoft.AspNetCore.Mvc;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;

namespace UrunSatisSitesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IRepository<AppUser> _repository;

        public LoginController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            var kullanici = await _repository.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive);

            if (kullanici == null)
            {
                TempData["mesaj"] = "<div class='alert alert-danger'>Giriş Başarısız!</div>";
            }
            else
            {

            }

            return View();
        }
    }
}
