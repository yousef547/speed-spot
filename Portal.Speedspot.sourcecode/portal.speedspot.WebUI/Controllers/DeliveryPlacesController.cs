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
    public class DeliveryPlacesController : BaseController
    {
        private readonly DeliveryPlacesBiz _deliveryPlacesBiz;

        public DeliveryPlacesController(DeliveryPlacesBiz deliveryPlacesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _deliveryPlacesBiz = deliveryPlacesBiz;
        }
        [Authorize(Permissions.DeliveryPlaces.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetDeliveryPlaces()
        {
            List<DeliveryPlaceViewModel> DeliveryPlaces = _mapper.Map<List<DeliveryPlaceViewModel>>(await _deliveryPlacesBiz.GetAllAsync());
            return PartialView("_ShowDeliveryPlacePartial", DeliveryPlaces);
        }

        [Authorize(Permissions.DeliveryPlaces.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.DeliveryPlaces.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(DeliveryPlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                DeliveryPlace type = _mapper.Map<DeliveryPlace>(model);
                bool result = await _deliveryPlacesBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                } 
            }
            return View(model);
        }
        [Authorize(Permissions.DeliveryPlaces.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<DeliveryPlaceViewModel>(await _deliveryPlacesBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.DeliveryPlaces.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(DeliveryPlaceViewModel model)
        {
            var deliveryPlace = _mapper.Map<DeliveryPlace>(await _deliveryPlacesBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            deliveryPlace.Name = model.Name;
            deliveryPlace.NameAr = model.NameAr;
            bool result = await _deliveryPlacesBiz.UpdateAsync(deliveryPlace);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.DeliveryPlaces.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _deliveryPlacesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }
        [Authorize(Permissions.DeliveryPlaces.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
           var result = await _deliveryPlacesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
