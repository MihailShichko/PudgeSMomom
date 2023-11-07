using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.Models.AdvertModels;
using PudgeSMomom.Services.Cloudinary;
using PudgeSMomom.Services.Repository.AdvertRepository;
using PudgeSMomom.ViewModels.AdvertVMs;

namespace PudgeSMomom.Controllers
{
    public class AdvertController : Controller
    {
        IAdvertRepository _advertRepository;
        IPhotoService _photoService;
        public AdvertController(IAdvertRepository advertRepository, IPhotoService photoService)
        {
            this._advertRepository = advertRepository;
            this._photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _advertRepository.GetAdverts());
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id);
            return View(advert);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertVM advertVM)
        {
            if (ModelState.IsValid) {
                var result = await _photoService.AddPhotoAsycn(advertVM.Image);
                var advert = new Advert
                {
                    Title = advertVM.Title,
                    Description = advertVM.Description,
                    Image = result.Uri.ToString()
                };

                _advertRepository.AddAdvert(advert);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }
            
            return View(advertVM);
        }

    }
}
