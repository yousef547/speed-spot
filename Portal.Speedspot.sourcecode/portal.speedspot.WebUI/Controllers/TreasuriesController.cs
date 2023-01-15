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
    public class TreasuriesController : BaseController
    {
        private readonly TreasuriesBiz _treasuriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly BanksBiz _banksBiz;
        private readonly CurrenciesBiz _currenciesBiz;

        public TreasuriesController(
            ApplicationUserManager userManager,
            IMapper mapper,
            LocalizationService localizationService,
            TreasuriesBiz treasuriesBiz,
            DepartmentsBiz departmentsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            BanksBiz banksBiz,
            CurrenciesBiz currenciesBiz)
            : base(mapper, userManager, localizationService)
        {
            _treasuriesBiz = treasuriesBiz;
            _departmentsBiz = departmentsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _banksBiz = banksBiz;
            _currenciesBiz = currenciesBiz;
        }


        [Authorize(Permissions.Treasuries.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            if (await _userManager.IsSuperAdmin(user))
            {
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedAsync();
            }
            else
            {
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(ud => ud.DepartmentId).ToList());
            }

            return View();
        }

        public async Task<IActionResult> GetTreasuries(int departmentId)
        {
            var treasuries = _mapper.Map<List<TreasuryViewModel>>(await _treasuriesBiz.GetUnarchivedAsync(departmentId));

            ViewData["CashGroup"] = treasuries.Where(t => t.Type == ApplicationEnums.TreasuryType.Cash).GroupBy(t => t.CurrencyId).ToList();
            ViewData["BankGroup"] = treasuries.Where(t => t.Type == ApplicationEnums.TreasuryType.BankAccount).GroupBy(t => t.CurrencyId).ToList();

            return PartialView("_TreasuriesPartial");
        }

        [Authorize(Permissions.Treasuries.Create)]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);
            if (userIsSuperAdmin)
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            }
            else
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());
            }

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();

            return View();
        }

        [Authorize(Permissions.Treasuries.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(TreasuryViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var addTreasury = false;

            if (model.Type == ApplicationEnums.TreasuryType.BankAccount)
            {
                if (model.BankId is null)
                {
                    ModelState.AddModelError("BankId", string.Format(_localizationService["RequiredField"].Value, _localizationService["BankId"].Value));
                }

                if (model.AccountNo is null)
                {
                    ModelState.AddModelError("AccountNo", string.Format(_localizationService["RequiredField"].Value, _localizationService["AccountNo"].Value));
                }
            }
            else
            {
                if (model.Name is null)
                {
                    ModelState.AddModelError("Name", string.Format(_localizationService["RequiredField"].Value, _localizationService["Name"].Value));
                }
            }

            if (ModelState.IsValid)
            {
                var treasury = _mapper.Map<Treasury>(model);
                treasury.DateCreated = System.DateTime.UtcNow;

                addTreasury = await _treasuriesBiz.AddAsync(treasury, user.Id);
            }

            if (addTreasury)
            {
                return RedirectToAction("Index");
            }

            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);
            if (userIsSuperAdmin)
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            }
            else
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());
            }

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<TreasuryViewModel>(await _treasuriesBiz.GetByIdAsync(id));

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);
            if (userIsSuperAdmin)
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            }
            else
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());
            }

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TreasuryViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (model.Type == ApplicationEnums.TreasuryType.BankAccount)
            {
                if (model.BankId is null)
                {
                    ModelState.AddModelError("BankId", string.Format(_localizationService["RequiredField"].Value, _localizationService["BankId"].Value));
                }

                if (model.AccountNo is null)
                {
                    ModelState.AddModelError("AccountNo", string.Format(_localizationService["RequiredField"].Value, _localizationService["AccountNo"].Value));
                }
            }
            else
            {
                if (model.Name is null)
                {
                    ModelState.AddModelError("Name", string.Format(_localizationService["RequiredField"].Value, _localizationService["Name"].Value));
                }
            }

            if (ModelState.IsValid)
            {
                var treasury = _mapper.Map<Treasury>(model);

                await _treasuriesBiz.UpdateAsync(treasury, user.Id);

                return RedirectToAction(nameof(Index));
            }

            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);
            if (userIsSuperAdmin)
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            }
            else
            {
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());
            }

            ViewData["CurrencyId"] = await _currenciesBiz.GetAllUnarchivedAsync();
            ViewData["BankId"] = await _banksBiz.GetAllUnarchivedAsync();

            return View(model);
        }

        [Authorize(Permissions.Treasuries.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _treasuriesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.Treasuries.Delete)]
        public async Task<IActionResult> UnArchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _treasuriesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
