using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly CitiesBiz _citiesBiz;
        private readonly CountriesBiz _countriesBiz;

        public CitiesController(CitiesBiz citiesBiz,
            CountriesBiz countriesBiz,
            IMapper mapper,
            ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _citiesBiz = citiesBiz;
            _countriesBiz = countriesBiz;
        }

        [Authorize(Permissions.Cities.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCities()
        {
            var cities = _mapper.Map<List<CityViewModel>>(await _citiesBiz.GetAllAsync());
            return PartialView("_ShowCitiesPartial", cities);
        }

        [Authorize(Permissions.Cities.Create)]
        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View();
        }

        [Authorize(Permissions.Cities.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var city = _mapper.Map<speedspot.Models.Concretes.City>(model);
                var result = await _citiesBiz.AddAsync(city, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Cities.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<CityViewModel>(await _citiesBiz.GetByIdAsync(id));

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Cities.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var city = _mapper.Map<speedspot.Models.Concretes.City>(model);
                var result = await _citiesBiz.UpdateAsync(city, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Cities.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _citiesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.Cities.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _citiesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
