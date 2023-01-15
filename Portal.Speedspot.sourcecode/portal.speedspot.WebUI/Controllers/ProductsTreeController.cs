using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;

namespace portal.speedspot.WebUI.Controllers
{
    public class ProductsTreeController : BaseController
    {
        private readonly ProductsBiz _productsBiz;
        private readonly ProductCategoriesBiz _productCategoriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;

        public ProductsTreeController(IMapper mapper,
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

        public async Task<IActionResult> GetProductsTree(int departmentId)
        {
            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(departmentId, false));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        public async Task<IActionResult> GetServicesTree(int departmentId)
        {
            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(departmentId, true));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        public async Task<IActionResult> GetCategoryDetails(int categoryId)
        {
            var category = _mapper.Map<ProductCategoryViewModel>(await _productCategoriesBiz.GetByIdAsync(categoryId));

            return Json(category, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productsBiz.GetByIdAsync(id));

            return Json(product, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(ProductCategoryViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.ParentId == null)
                model.AutoCode = await _productCategoriesBiz.GetNewParentAutoCode(model.DepartmentId);
            else
                model.AutoCode = await _productCategoriesBiz.GetNewAutoCode((int)model.ParentId);

            var category = _mapper.Map<ProductCategory>(model);
            await _productCategoriesBiz.AddAsync(category, user.Id);

            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(model.DepartmentId, model.IsService));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.AutoCode = await _productsBiz.GetNewAutoCode(model.CategoryId);
            var product = _mapper.Map<Product>(model);
            await _productsBiz.AddAsync(product, user.Id);

            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(model.DepartmentId, model.IsService));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(ProductCategoryViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var category = _mapper.Map<ProductCategory>(model);
            await _productCategoriesBiz.UpdateAsync(category, user.Id);

            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(model.DepartmentId, model.IsService));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var product = _mapper.Map<Product>(model);
            await _productsBiz.UpdateAsync(product, user.Id);

            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(model.DepartmentId, model.IsService));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        public async Task<IActionResult> DeleteCategory(int categoryId, bool isService, int departmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _productCategoriesBiz.ArchiveAsync(categoryId, user.Id);

            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(departmentId, isService));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        public async Task<IActionResult> DeleteProduct(int id, bool isService, int departmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _productsBiz.ArchiveAsync(id, user.Id);

            var productsTree = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetParentsByDepartmentId(departmentId, isService));
            return PartialView("_CategoriesTreePartial", productsTree);
        }

        public async Task<IActionResult> GenerateNewCode(int departmentId)
        {
            string generatedCode = await _productCategoriesBiz.GenerateNewParentCode(departmentId);

            return Json(generatedCode);
        }

        public async Task<IActionResult> GenerateNewCategoryCode(int parentId)
        {
            var parentVM = _mapper.Map<ProductCategoryViewModel>(await _productCategoriesBiz.GetByIdAsync(parentId));
            string autoCode = await _productCategoriesBiz.GetNewAutoCode(parentId);
            return Json(parentVM.GeneratedCode + autoCode);
        }

        public async Task<IActionResult> GenerateNewProductCode(int categoryId)
        {
            var parentVM = _mapper.Map<ProductCategoryViewModel>(await _productCategoriesBiz.GetByIdAsync(categoryId));
            string autoCode = await _productsBiz.GetNewAutoCode(categoryId);
            return Json(parentVM.GeneratedCode + autoCode);
        }

        [Authorize(Permissions.ProductsTree.View)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            var departments = new List<speedspot.Models.Concretes.Department>();
            if (await _userManager.IsSuperAdmin(user))
                departments = (await _departmentsBiz.GetAllUnarchivedAsync()).ToList();
            else
                departments = (await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList())).ToList();

            ViewData["Departments"] = departments;

            return View();
        }
    }
}
