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
    public class ChartOfAccountsController : BaseController
    {
        private readonly FinancialAccountsBiz _financialAccountsBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly FinancialAccountTransactionsBiz _financialAccountTransactionsBiz;

        public ChartOfAccountsController(FinancialAccountsBiz financialAccountsBiz,
            DepartmentsBiz departmentsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            IMapper mapper,
            ApplicationUserManager userManager,
            FinancialAccountTransactionsBiz financialAccountTransactionsBiz) : base(mapper, userManager)
        {
            _financialAccountsBiz = financialAccountsBiz;
            _departmentsBiz = departmentsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _financialAccountTransactionsBiz = financialAccountTransactionsBiz;
        }

        public async Task<IActionResult> GetIncomeTree(int departmentId)
        {
            var incomeTree = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetParentsByDepartmentId(departmentId,
                ApplicationEnums.FinancialAccountType.Income));
            return PartialView("_ParentsPartial", incomeTree);
        }

        public async Task<IActionResult> GetExpenseTree(int departmentId)
        {
            var expenseTree = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetParentsByDepartmentId(departmentId,
               ApplicationEnums.FinancialAccountType.Expense));
            return PartialView("_ParentsPartial", expenseTree);
        }

        public async Task<IActionResult> GenerateNewParentCode(int departmentId, ApplicationEnums.FinancialAccountType type)
        {
            string generatedCode = await _financialAccountsBiz.GenerateNewParentCode(departmentId, type);

            return Json(generatedCode);
        }

        public async Task<IActionResult> GenerateNewCode(int parentId)
        {
            var parentVM = _mapper.Map<FinancialAccountViewModel>(await _financialAccountsBiz.GetByIdAsync(parentId));
            string autoCode = await _financialAccountsBiz.GetNewAutoCode(parentVM.Id);
            return Json(parentVM.GeneratedCode + autoCode);
        }

        public async Task<IActionResult> GetAccountInfo(int id)
        {
            var model = _mapper.Map<FinancialAccountViewModel>(await _financialAccountsBiz.GetByIdAsync(id));

            return PartialView("_AccountInfoModal", model);
        }

        [Authorize(Permissions.ChartOfAccounts.View)]
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

        [Authorize(Permissions.ChartOfAccounts.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(FinancialAccountViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.ParentId == null)
                model.AutoCode = await _financialAccountsBiz.GetNewParentAutoCode(model.DepartmentId, model.Type);
            else
                model.AutoCode = await _financialAccountsBiz.GetNewAutoCode((int)model.ParentId);

            var account = _mapper.Map<FinancialAccount>(model);
            await _financialAccountsBiz.AddAsync(account, user.Id);
            var treeData = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetParentsByDepartmentId(model.DepartmentId, model.Type));
            return PartialView("_ParentsPartial", treeData);
        }

        [Authorize(Permissions.ChartOfAccounts.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(FinancialAccountViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var account = _mapper.Map<FinancialAccount>(model);
            await _financialAccountsBiz.UpdateAsync(account, user.Id);
            var treeData = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetParentsByDepartmentId(model.DepartmentId, model.Type));
            return PartialView("_ParentsPartial", treeData);
        }

        [Authorize(Permissions.ChartOfAccounts.Details)]
        public async Task<IActionResult> Details(int id)
        {
            var model = _mapper.Map<FinancialAccountViewModel>(await _financialAccountsBiz.GetByIdAsync(id));

            return PartialView("_ViewInBox", model);
        }

        [Authorize(Permissions.ChartOfAccounts.Delete)]
        public async Task<IActionResult> Delete(int id, int departmentId, ApplicationEnums.FinancialAccountType type)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _financialAccountsBiz.ArchiveAsync(id, user.Id);

            var treeData = _mapper.Map<List<FinancialAccountViewModel>>(await _financialAccountsBiz.GetParentsByDepartmentId(departmentId, type));
            return PartialView("_ParentsPartial", treeData);
        }

        [Authorize(Permissions.ChartOfAccounts.Report)]
        public async Task<IActionResult> Report(int id)
        {
            var transactions = _mapper.Map<List<FinancialAccountTransactionViewModel>>(await _financialAccountTransactionsBiz.GetByAccountIdAsync(id));

            return View(transactions);
        }
    }
}
