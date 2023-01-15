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
    public class CurrenciesController : BaseController
    {
        private readonly CurrenciesBiz _currenciesBiz;

        public CurrenciesController(CurrenciesBiz currenciesBiz,
            ApplicationUserManager userManager,
            IMapper mapper) : base(mapper, userManager)
        {
            _currenciesBiz = currenciesBiz;
        }

        [Authorize(Permissions.Currencies.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCurrencies()
        {
            List<CurrencyViewModel> currencies =  _mapper.Map<List<CurrencyViewModel>>(await _currenciesBiz.GetAllAsync());
            return PartialView("_ShowCurrenciesPartial", currencies);
        }

        [Authorize(Permissions.Currencies.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.Currencies.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var currency = _mapper.Map<Currency>(model);
                var result = await _currenciesBiz.AddAsync(currency, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Currencies.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            CurrencyViewModel model = _mapper.Map<CurrencyViewModel>(await _currenciesBiz.GetByIdAsync(id));
            return View(model);
        }

        [Authorize(Permissions.Currencies.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var currency = _mapper.Map<Currency>(model);
                var result = await _currenciesBiz.UpdateAsync(currency, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Currencies.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _currenciesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.Currencies.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _currenciesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
