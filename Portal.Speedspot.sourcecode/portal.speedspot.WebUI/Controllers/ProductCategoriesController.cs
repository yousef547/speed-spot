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
    public class ProductCategoriesController : BaseController
    {
        private readonly ProductCategoriesBiz _productCategoriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;

        public ProductCategoriesController(IMapper mapper,
            ProductCategoriesBiz productCategoriesBiz,
            DepartmentsBiz departmentsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            ApplicationUserManager userManager,
            LocalizationService localizationService)
            : base(mapper, userManager, localizationService)
        {
            _productCategoriesBiz = productCategoriesBiz;
            _departmentsBiz = departmentsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
        }

        public async Task<IActionResult> GetByDepartments(List<int> departmentIds, bool isService)
        {
            var categories = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetCategoriesWithChilds(departmentIds, isService)).Where(c => c.ParentId == null).ToList();

            List<IGrouping<string, ProductCategoryViewModel>> groups;
            if (_localizationService.IsRightToLeft())
            {
                groups = categories.GroupBy(g => g.DepartmentNameAr).ToList();
            }
            else
            {
                groups = categories.GroupBy(g => g.DepartmentName).ToList();
            }
            return PartialView("_CategoriesSelectPartial", groups);
        }

        public async Task<IActionResult> GetParentsByDepartmentId(int id, bool isService)
        {
            var parents = await _productCategoriesBiz.GetByDepartmentId(id, isService);

            return PartialView("_ParentsPartial", parents);
        }

        [Authorize(Permissions.ProductCategories.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            List<ProductCategoryViewModel> productCategories;
            if (userIsSuperAdmin)
                productCategories = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetAllAsync());
            else
                productCategories = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetAllByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));

            return View(productCategories);
        }

        [Authorize(Permissions.ProductCategories.Create)]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["ParentId"] = new List<ProductCategory>();

            return View(new ProductCategoryViewModel());
        }

        [Authorize(Permissions.ProductCategories.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var category = _mapper.Map<ProductCategory>(model);
                var result = await _productCategoriesBiz.AddAsync(category, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            if (model.DepartmentId != 0)
            {
                ViewData["ParentId"] = (await _productCategoriesBiz.GetByDepartmentId(model.DepartmentId, model.IsService)).ToList();
            }
            else
            {
                ViewData["ParentId"] = new List<ProductCategory>();
            }

            return View(model);
        }

        [Authorize(Permissions.ProductCategories.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<ProductCategoryViewModel>(await _productCategoriesBiz.GetByIdAsync(id));

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["ParentId"] = (await _productCategoriesBiz.GetByDepartmentId(model.DepartmentId, model.IsService)).Where(c => c.Id != model.Id).ToList();

            return View(model);
        }

        [Authorize(Permissions.ProductCategories.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var category = _mapper.Map<ProductCategory>(model);
                var result = await _productCategoriesBiz.UpdateAsync(category, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            if (model.DepartmentId != 0)
            {
                ViewData["ParentId"] = (await _productCategoriesBiz.GetByDepartmentId(model.DepartmentId, model.IsService)).Where(c => c.Id != model.Id).ToList();
            }
            else
            {
                ViewData["ParentId"] = new List<ProductCategory>();
            }

            return View(model);
        }

        [Authorize(Permissions.ProductCategories.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productCategoriesBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.ProductCategories.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productCategoriesBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
