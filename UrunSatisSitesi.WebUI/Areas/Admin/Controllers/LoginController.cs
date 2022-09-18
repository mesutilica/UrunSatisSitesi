using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
                var kullaniciHaklari = new List<Claim>() // claim = hak
                {
                    new Claim(ClaimTypes.Email, kullanici.Email),
                    new Claim(ClaimTypes.Name, kullanici.Name),
                    new Claim("Role", kullanici.IsAdmin ? "Admin" : "User"),
                    new Claim("UserId", kullanici.Id.ToString())
                };

                var kullaniciKimligi = new ClaimsIdentity(kullaniciHaklari, "Login");

                ClaimsPrincipal principal = new(kullaniciKimligi);

                await HttpContext.SignInAsync(principal);

                return Redirect("/Admin");
            }

            return View();
        }

        [Route("Admin/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
