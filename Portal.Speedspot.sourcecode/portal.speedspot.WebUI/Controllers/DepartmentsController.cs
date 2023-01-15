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
    public class DepartmentsController : BaseController
    {
        private readonly DepartmentsBiz _departmentsBiz;

        public DepartmentsController(DepartmentsBiz departmentsBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _departmentsBiz = departmentsBiz;
        }

        [Authorize(Permissions.Departments.View)]
        public async Task<IActionResult> Index()
        {
            var departments = (await _departmentsBiz.GetAllAsync());
            return View(departments);
        }

        [Authorize(Permissions.Departments.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Permissions.Departments.Create)]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var department = _mapper.Map<Department>(model);
                var result = await _departmentsBiz.AddAsync(department, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Departments.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentsBiz.GetByIdAsync(id);
            var model = _mapper.Map<DepartmentViewModel>(department);
            return View(model);
        }

        [Authorize(Permissions.Departments.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var department = _mapper.Map<Department>(model);
                var result = await _departmentsBiz.UpdateAsync(department, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Departments.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _departmentsBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Departments.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _departmentsBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
