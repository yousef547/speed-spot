using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;

namespace portal.speedspot.WebUI.Controllers
{
    public class ProductCategoriesArchiveController : BaseController
    {
        private readonly ProductCategoriesBiz _productCategoriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;

        public ProductCategoriesArchiveController(IMapper mapper,
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadProductCategories()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            bool userIsSuperAdmin = await _userManager.IsSuperAdmin(user);

            List<ProductCategoryViewModel> productCategories;
            if (userIsSuperAdmin)
                productCategories = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetAllArchivedAsync());
            else
                productCategories = _mapper.Map<List<ProductCategoryViewModel>>(await _productCategoriesBiz.GetAllArchivedByUserAsync(user.DepartmentId,
                    userDepartments.Select(x => x.DepartmentId).ToList()));

            return PartialView("_TablePartial", productCategories);
        }

        [Authorize(Permissions.ProductCategories.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _productCategoriesBiz.DeleteAsync(id, user.Id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.ProductCategories.Delete)]
        public async Task<IActionResult> Restore(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productCategoriesBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
