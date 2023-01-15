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
    public class ProductsController : BaseController
    {
        private readonly ProductsBiz _productsBiz;
        private readonly ProductCategoriesBiz _productCategoriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;

        public ProductsController(IMapper mapper,
            ProductsBiz productsBiz,
            DepartmentsBiz departmentsBiz,
            ProductCategoriesBiz productCategoriesBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            ApplicationUserManager userManager)
            : base(mapper, userManager)
        {
            _productsBiz = productsBiz;
            _productCategoriesBiz = productCategoriesBiz;
            _departmentsBiz = departmentsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
        }

        public async Task<IActionResult> GetCategoriesByDepartmentId(int id, bool isService)
        {
            var categories = await _productCategoriesBiz.GetByDepartmentId(id, isService);

            return PartialView("_CategoriesPartial", categories);
        }

        [Authorize(Permissions.Products.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            List<ProductViewModel> products;
            if (userIsSuperAdmin)
                products = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllAsync());
            else
                products = _mapper.Map<List<ProductViewModel>>(await _productsBiz.GetAllByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));

            return View(products);
        }

        [Authorize(Permissions.Products.Create)]
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

            ViewData["CategoryId"] = new List<ProductCategory>();

            return View(new ProductViewModel());
        }

        [Authorize(Permissions.Products.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var product = _mapper.Map<Product>(model);
                var result = await _productsBiz.AddAsync(product, userId);
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
                ViewData["CategoryId"] = (await _productCategoriesBiz.GetByDepartmentId(model.DepartmentId, model.IsService)).ToList();
            }
            else
            {
                ViewData["CategoryId"] = new List<ProductCategory>();
            }

            return View(model);
        }

        [Authorize(Permissions.Products.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<ProductViewModel>(await _productsBiz.GetByIdAsync(id));

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);
            if (userIsSuperAdmin)
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList());

            ViewData["CategoryId"] = (await _productCategoriesBiz.GetByDepartmentId(model.DepartmentId, model.IsService)).ToList();

            return View(model);
        }

        [Authorize(Permissions.Products.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var product = _mapper.Map<Product>(model);
                var result = await _productsBiz.UpdateAsync(product, userId);
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
                ViewData["CategoryId"] = (await _productCategoriesBiz.GetByDepartmentId(model.DepartmentId, model.IsService)).ToList();
            }
            else
            {
                ViewData["CategoryId"] = new List<ProductCategory>();
            }

            return View(model);
        }

        [Authorize(Permissions.Products.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productsBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Products.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productsBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
