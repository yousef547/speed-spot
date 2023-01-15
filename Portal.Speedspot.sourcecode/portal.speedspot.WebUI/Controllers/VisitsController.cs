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
using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;
using static portal.speedspot.Models.Constants.UserActivities;

namespace portal.speedspot.WebUI.Controllers
{
    public class VisitsController : BaseController
    {
        private readonly VisitsBiz _visitsBiz;
        private readonly VisitReasonsBiz _visitReasonsBiz;
        private readonly CustomersBiz _customersBiz;
        private readonly EmployeeTypesBiz _employeeTypesBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly DepartmentsBiz _departmentsBiz;

        public VisitsController(VisitsBiz visitsBiz,
            VisitReasonsBiz visitReasonsBiz,
            CustomersBiz customersBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            DepartmentsBiz departmentsBiz,
            EmployeeTypesBiz employeeTypesBiz,
            IMapper mapper,
            ApplicationUserManager userManager,
            UserActivitiesBiz userActivitiesBiz) : base(mapper, userManager, userActivitiesBiz)
        {
            _visitsBiz = visitsBiz;
            _visitReasonsBiz = visitReasonsBiz;
            _customersBiz = customersBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _employeeTypesBiz = employeeTypesBiz;
            _departmentsBiz = departmentsBiz;
        }

        public async Task LoadViewData()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            if (await _userManager.IsSuperAdmin(user))
                ViewData["CustomerId"] = await _customersBiz.GetAllUnarchivedAsync();
            else
                ViewData["CustomerId"] = await _customersBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["ReasonId"] = await _visitReasonsBiz.GetAllUnarchivedAsync();
        }

        [Authorize(Permissions.Visits.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            List<int> userDepartmentIds = userDepartments.Select(d => d.DepartmentId).ToList();
            if (await _userManager.IsSuperAdmin(user))
            {
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedAsync();
                ViewData["CustomerId"] = await _customersBiz.GetAllUnarchivedAsync();
            }
            else
            {
                ViewData["Departments"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartmentIds);
                ViewData["CustomerId"] = await _customersBiz.GetAllUnarchivedByUserAsync(user.Id, user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());
            }
            return View();
        }

        public async Task<IActionResult> GetVisits()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            List<int> userDepartmentIds = userDepartments.Select(d => d.DepartmentId).ToList();
            var salesAgentEmployeeType = await _employeeTypesBiz.GetByEnumValue((int)EmployeeTypeEnum.SalesAgent);
            List<VisitViewModel> model;
            if (user.EmployeeTypeId == salesAgentEmployeeType.Id)
            {
                model = _mapper.Map<List<VisitViewModel>>(await _visitsBiz.GetBySalesAgentAsync(user.Id));
            }
            else
            {
                model = _mapper.Map<List<VisitViewModel>>(await _visitsBiz.GetByUserAsync(user.Id, user.DepartmentId, userDepartmentIds));
            }
            return PartialView("_ShowVisitsPartial", model);

        }

        [Authorize(Permissions.Visits.Create)]
        public async Task<IActionResult> Create()
        {
            await LoadViewData();
            return View();
        }

        [Authorize(Permissions.Visits.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(VisitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                model.SalesAgentId = user.Id;
                Visit visit = _mapper.Map<Visit>(model);
                bool result = await _visitsBiz.AddAsync(visit, user.Id);
                if (result)
                {
                    var customer = await _customersBiz.GetInfoByIdAsync(visit.CustomerId);
                    await SaveUserActivity(user.Id, UserActivities.Visits.Create, customer.Name);

                    return RedirectToAction(nameof(Index));
                }
            }

            await LoadViewData();
            return View(model);
        }

        [Authorize(Permissions.Visits.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            VisitViewModel model = _mapper.Map<VisitViewModel>(await _visitsBiz.GetByIdAsync(id));

            await LoadViewData();
            return View(model);
        }

        [Authorize(Permissions.Visits.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(VisitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                Visit visit = _mapper.Map<Visit>(model);
                bool result = await _visitsBiz.UpdateAsync(visit, user.Id);
                if (result)
                {
                    var customer = await _customersBiz.GetInfoByIdAsync(visit.CustomerId);
                    await SaveUserActivity(user.Id, UserActivities.Visits.Update, customer.Name);

                    return RedirectToAction(nameof(Index));
                }
            }

            await LoadViewData();
            return View(model);
        }

        [Authorize(Permissions.Visits.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _visitsBiz.ArchiveAsync(id, userId);

            var visit = await _visitsBiz.GetByIdAsync(id);
            await SaveUserActivity(userId, UserActivities.Visits.Archive, visit.Customer.Name);

            return Json(result);
        }

        [Authorize(Permissions.Visits.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var result = await _visitsBiz.UnarchiveAsync(id, userId);

            var visit = await _visitsBiz.GetByIdAsync(id);
            await SaveUserActivity(userId, UserActivities.Visits.Unarchive, visit.Customer.Name);

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            VisitViewModel model = _mapper.Map<VisitViewModel>(await _visitsBiz.GetByIdAsync(id));
            return PartialView("_VisitDetailsPartial", model);
        }
    }
}
