using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class CountriesController : BaseController
    {
        private readonly CountriesBiz _countriesBiz;

        public CountriesController(CountriesBiz countriesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _countriesBiz = countriesBiz;
        }

        [Authorize(Permissions.Countries.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCountries()
        {
            var countries = _mapper.Map<List<CountryViewModel>>(await _countriesBiz.GetAllAsync());
            return PartialView("_ShowCountriesPartial",countries);
        }

        [Authorize(Permissions.Countries.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Permissions.Countries.Create)]
        public async Task<IActionResult> Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var country = _mapper.Map<Country>(model);
                var result = await _countriesBiz.AddAsync(country, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Countries.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<CountryViewModel>(await _countriesBiz.GetByIdAsync(id));

            return View(model);
        }

        [Authorize(Permissions.Countries.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var country = _mapper.Map<Country>(model);
                var result = await _countriesBiz.UpdateAsync(country, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Countries.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _countriesBiz.ArchiveAsync(id, userId);

            return Json(result);
        }

        [Authorize(Permissions.Countries.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _countriesBiz.UnarchiveAsync(id, userId);

            return Json(result);
        }
    }
}
