using Microsoft.AspNetCore.Mvc;
using UrunSatisSitesi.Entities;
using UrunSatisSitesi.Service.Repositories;

namespace UrunSatisSitesi.WebUI.ViewComponents
{
    public class Categories : ViewComponent // ViewComponent sınıfı menü modülü için kullanacağımız .net core yapısı
    {
        private readonly IRepository<Category> _repository;

        public Categories(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _repository.GetAllAsync()); // Views > Shared > Components > Categories içerisindeki default sayfasına model verisini yolluyoruz. Son olarak bu yaptığımız componenti Layout içerisinde çağırmamız gerekiyor
        }

    }
}
