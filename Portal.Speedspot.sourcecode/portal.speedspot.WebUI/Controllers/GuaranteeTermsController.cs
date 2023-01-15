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
    public class GuaranteeTermsController : BaseController
    {
        private readonly GuaranteeTermsBiz _guaranteeTermsBiz;

        public GuaranteeTermsController(GuaranteeTermsBiz guaranteeTermsBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _guaranteeTermsBiz = guaranteeTermsBiz;
        }

        [Authorize(Permissions.GuaranteeTerms.View)]
        public IActionResult Index()
        {
          
            return View();
        }


        public async Task<IActionResult> GetGuaraneeTerms()
        {
            List<GuaranteeTermViewModel> GuaranteeTerms = _mapper.Map<List<GuaranteeTermViewModel>>(await _guaranteeTermsBiz.GetAllAsync());

            return PartialView("_ShowAllGuaraneeTermsPartial", GuaranteeTerms);
        }

        [Authorize(Permissions.GuaranteeTerms.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.GuaranteeTerms.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(GuaranteeTermViewModel model)
        {
            if (ModelState.IsValid)
            {
                GuaranteeTerm type = _mapper.Map<GuaranteeTerm>(model);
                bool result = await _guaranteeTermsBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [Authorize(Permissions.GuaranteeTerms.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<GuaranteeTermViewModel>(await _guaranteeTermsBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.GuaranteeTerms.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(GuaranteeTermViewModel model)
        {
            var GuaranteeTerm = _mapper.Map<GuaranteeTerm>(await _guaranteeTermsBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            GuaranteeTerm.Name = model.Name;
            GuaranteeTerm.NameAr = model.NameAr;
            bool result = await _guaranteeTermsBiz.UpdateAsync(GuaranteeTerm);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.GuaranteeTerms.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var resulit = await _guaranteeTermsBiz.ArchiveAsync(id, userId);
            return Json(resulit);
        }
        [Authorize(Permissions.GuaranteeTerms.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var resulit=  await _guaranteeTermsBiz.UnarchiveAsync(id, userId);
            return Json(resulit);
        }
    }
}
