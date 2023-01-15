using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class ClassificationsController : BaseController
    {
        private readonly ClassificationsBiz _classificationsBiz;

        public ClassificationsController(ClassificationsBiz classificationsBiz,
            IMapper mapper,
            ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _classificationsBiz = classificationsBiz;
        }

        [Authorize(Permissions.Classifications.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetClassifications()
        {
            List<ClassificationViewModel> classifications = _mapper.Map<List<ClassificationViewModel>>(await _classificationsBiz.GetAllAsync());
            return PartialView("_ShowClassificationsPartial",classifications);
            
        }

        [Authorize(Permissions.Classifications.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.Classifications.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(ClassificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var classification = _mapper.Map<Classification>(model);
                var result = await _classificationsBiz.AddAsync(classification, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Classifications.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<ClassificationViewModel>(await _classificationsBiz.GetByIdAsync(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClassificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var classification = _mapper.Map<Classification>(model);
                var result = await _classificationsBiz.UpdateAsync(classification, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Classifications.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _classificationsBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.Classifications.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _classificationsBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
