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
    public class VATValuesController : BaseController
    {
        private readonly VATValuesBiz _vatValuesBiz;

        public VATValuesController(VATValuesBiz vatValuesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _vatValuesBiz = vatValuesBiz;
        }

        [Authorize(Permissions.VATValues.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetVATValues()
        {
            List<VATValueViewModel> VATValues = _mapper.Map<List<VATValueViewModel>>(await _vatValuesBiz.GetAllAsync());
            return PartialView("_ShowVATValuePartial", VATValues);
        }

        [Authorize(Permissions.VATValues.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.VATValues.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(VATValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                VATValue type = _mapper.Map<VATValue>(model);
                bool result = await _vatValuesBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [Authorize(Permissions.VATValues.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<VATValueViewModel>(await _vatValuesBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize(Permissions.VATValues.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(VATValueViewModel model)
        {
            var vATValue = _mapper.Map<VATValue>(await _vatValuesBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            vATValue.Title = model.Title;
            vATValue.Value = model.Value;
            bool result = await _vatValuesBiz.UpdateAsync(vATValue);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Permissions.VATValues.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _vatValuesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.VATValues.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _vatValuesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
