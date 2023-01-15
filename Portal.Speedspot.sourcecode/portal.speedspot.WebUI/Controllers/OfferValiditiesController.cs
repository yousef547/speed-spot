using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class OfferValiditiesController : BaseController
    {
        private readonly OfferValiditiesBiz _offerValiditiesBiz;

        public OfferValiditiesController(OfferValiditiesBiz offerValiditiesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _offerValiditiesBiz = offerValiditiesBiz;
        }
        [Authorize(Permissions.OfferValidities.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOfferValidities()
        {
            List<OfferValiditiesViewModel> OfferValidities = _mapper.Map<List<OfferValiditiesViewModel>>(await _offerValiditiesBiz.GetAllAsync());

            return PartialView("_ShowOfferVailditiesPartial", OfferValidities);
        }

        [Authorize(Permissions.OfferValidities.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.OfferValidities.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(OfferValiditiesViewModel model)
        {
            if (ModelState.IsValid)
            {
                OfferValidity type = _mapper.Map<OfferValidity>(model);
                bool result = await _offerValiditiesBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [Authorize(Permissions.OfferValidities.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<OfferValiditiesViewModel>(await _offerValiditiesBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.OfferValidities.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(OfferValiditiesViewModel model)
        {
            var OfferValidity = _mapper.Map<OfferValidity>(await _offerValiditiesBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            OfferValidity.Name = model.Name;
            OfferValidity.NameAr = model.NameAr;
            bool result = await _offerValiditiesBiz.UpdateAsync(OfferValidity);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.OfferValidities.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result =  await _offerValiditiesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }
        [Authorize(Permissions.OfferValidities.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _offerValiditiesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }

    }
}
