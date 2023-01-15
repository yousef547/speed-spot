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
    public class BanksController : BaseController
    {
        private readonly BanksBiz _banksBiz;
        private readonly CountriesBiz _countriesBiz;

        public BanksController(BanksBiz banksBiz, CountriesBiz countriesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _banksBiz = banksBiz;
            _countriesBiz = countriesBiz;
        }

        public class BranchesViewModel
        {
            public List<BankBranchViewModel> Branches { get; set; }

            public BranchesViewModel()
            {
                Branches = new List<BankBranchViewModel>();
            }
        }

        public IActionResult AddBranch(BranchesViewModel model)
        {
            if (model == null || model.Branches == null)
                model = new BranchesViewModel();

            model.Branches.Add(new BankBranchViewModel());

            return PartialView("_BranchesPartial", model.Branches);
        }

        public IActionResult DeleteBranch(BranchesViewModel model, int index)
        {
            model.Branches.RemoveAt(index);

            return PartialView("_BranchesPartial", model.Branches);
        }

        [Authorize(Permissions.Banks.View)]
        public async Task<IActionResult> Index()
        {
            var banks = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllAsync());
            return View(banks);
        }

        [Authorize(Permissions.Banks.Create)]
        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(new BankViewModel());
        }

        [HttpPost]
        [Authorize(Permissions.Banks.Create)]
        public async Task<IActionResult> Create(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var bank = _mapper.Map<Bank>(model);
                var result = await _banksBiz.AddAsync(bank, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Banks.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<BankViewModel>(await _banksBiz.GetByIdAsync(id));

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Banks.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var bank = _mapper.Map<Bank>(model);
                var result = await _banksBiz.UpdateAsync(bank, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CountryId"] = await _countriesBiz.GetAllUnarchivedAsync();
            return View(model);
        }

        [Authorize(Permissions.Banks.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _banksBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Banks.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _banksBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
