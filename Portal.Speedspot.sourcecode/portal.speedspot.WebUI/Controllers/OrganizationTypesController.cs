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
    public class OrganizationTypesController : BaseController
    {
        private readonly OrganizationTypesBiz _organizationTypesBiz;

        public OrganizationTypesController(OrganizationTypesBiz organizationTypesBiz,
            IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _organizationTypesBiz = organizationTypesBiz;
        }

        [Authorize(Permissions.OrganizationTypes.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrganizationTypes()
        {
            List<OrganizationTypeViewModel> organizationTypes = _mapper.Map<List<OrganizationTypeViewModel>>(await _organizationTypesBiz.GetAllAsync());
            return PartialView("_ShowOrganizationTypesPartial", organizationTypes);
        }

        [Authorize(Permissions.OrganizationTypes.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.OrganizationTypes.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(OrganizationTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                OrganizationType type = _mapper.Map<OrganizationType>(model);
                bool result = await _organizationTypesBiz.AddAsync(type, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.OrganizationTypes.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<OrganizationTypeViewModel>(await _organizationTypesBiz.GetByIdAsync(id));
            return View(model);
        }

        [Authorize(Permissions.OrganizationTypes.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(OrganizationTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                OrganizationType type = _mapper.Map<OrganizationType>(model);
                bool result = await _organizationTypesBiz.UpdateAsync(type, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.OrganizationTypes.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _organizationTypesBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.OrganizationTypes.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _organizationTypesBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
