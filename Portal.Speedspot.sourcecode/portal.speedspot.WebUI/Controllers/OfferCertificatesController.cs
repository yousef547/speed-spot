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
    public class OfferCertificatesController : BaseController
    {
        private readonly OfferCertificatesBiz _offerCertificatesBiz;

        public OfferCertificatesController(OfferCertificatesBiz offerCertificatesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _offerCertificatesBiz = offerCertificatesBiz;
        }

        [Authorize(Permissions.OfferCertificates.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOfferCertificate()
        {
            List<OfferCertificateViewModel> OfferCertificates = _mapper.Map<List<OfferCertificateViewModel>>(await _offerCertificatesBiz.GetAllAsync());
            return PartialView("_ShowOfferCertificPartial", OfferCertificates);
        }

        [Authorize(Permissions.OfferCertificates.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.OfferCertificates.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(OfferCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                OfferCertificate type = _mapper.Map<OfferCertificate>(model);
                bool result = await _offerCertificatesBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [Authorize(Permissions.OfferCertificates.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<OfferCertificateViewModel>(await _offerCertificatesBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.OfferCertificates.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(OfferCertificateViewModel model)
        {
            var OfferCertificate = _mapper.Map<OfferCertificate>(await _offerCertificatesBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            OfferCertificate.Name = model.Name;
            OfferCertificate.NameAr = model.NameAr;
            bool result = await _offerCertificatesBiz.UpdateAsync(OfferCertificate);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.OfferCertificates.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _offerCertificatesBiz.ArchiveAsync(id, userId);
            return Json(result);

        }
        [Authorize(Permissions.OfferCertificates.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _offerCertificatesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
