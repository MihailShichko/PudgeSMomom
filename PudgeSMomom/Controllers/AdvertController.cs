using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PudgeSMomom.Models.AdvertModels;
using PudgeSMomom.Services.Cloudinary;
using PudgeSMomom.Services.Repository.AdvertRepository;
using PudgeSMomom.Services.Repository.UserRepository;
using PudgeSMomom.ViewModels.AccountVMs;
using PudgeSMomom.ViewModels.AdvertVMs;

namespace PudgeSMomom.Controllers
{
    public class AdvertController : Controller
    {
        IAdvertRepository _advertRepository;
        IPhotoService _photoService;
        IUserRepository _userRepository;
        public AdvertController(IAdvertRepository advertRepository, IPhotoService photoService, IUserRepository userRepository)
        {
            this._advertRepository = advertRepository;
            this._photoService = photoService;
            this._userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            //if (TempData["Error"] != null)
            //{
            //    ViewData["ErrorMessage"] = TempData["Error"];

            //}
            return View(await _advertRepository.GetAdverts());
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id);
            return View(advert);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id);
            var user = await _userRepository.GetUserByNameAsync(User.Identity.Name.ToString());

            if (advert.User.Id != user.Id)
            {
                TempData["Error"] = "You can't edit someone else's advert";
                return RedirectToAction("Index");
            }

            if (advert == null) return View("Error");
            var advertVM = new EditAdvertVM
            {
                Title = advert.Title,
                Description = advert.Description,
                URL = advert.Image
            };

            return View(advertVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditAdvertVM advertVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit advert");
                return View("Edit", advertVM);
            }

            var advert = await _advertRepository.GetAdvertByIdAsync(id);

            if (advert != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(advert.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Can't delete photo");
                    return View(advertVM);
                }

                var result = await _photoService.AddPhotoAsycn(advertVM.Image);
                var newAdvert = new Advert
                {
                    Id = id,
                    Title = advertVM.Title,
                    Description = advertVM.Description,
                    Image = result.Uri.ToString()
                };

                _advertRepository.UpdateAdvert(newAdvert);
                return RedirectToAction("Index");
            }
            else
            {
                return View(advertVM);
            }
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
                    Image = result.Uri.ToString(),
                    User = await _userRepository.GetUserByNameAsync(User.Identity.Name.ToString())
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

        public async Task<IActionResult> Delete(int id)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id);

            var user = await _userRepository.GetUserByNameAsync(User.Identity.Name.ToString());

            if (advert.User.Id != user.Id)
            {
                TempData["Error"] = "You can't delete someone else's advert";
                return RedirectToAction("Index");
            }

            _advertRepository.DeleteAdvert(id);
            return RedirectToAction("Index");
        }

    }
}
