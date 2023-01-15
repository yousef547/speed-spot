using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductsArchiveController : BaseController
    {
        private readonly ProductsBiz _productsBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;

        public ProductsArchiveController(ProductsBiz productsBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            ApplicationUserManager userManager,
            IMapper mapper) : base(mapper, userManager)
        {
            _productsBiz = productsBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
        }

        [Authorize(Permissions.Products.ViewArchived)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadProducts()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            List<Product> products;
            if (await _userManager.IsSuperAdmin(user))
                products = (await _productsBiz.GetAllArchivedAsync()).ToList();
            else
                products = (await _productsBiz.GetAllArchivedByUserAsync(user.DepartmentId, userDepartments.Select(s => s.DepartmentId).ToList())).ToList();

            List<ProductViewModel> model = _mapper.Map<List<ProductViewModel>>(products);

            return PartialView("_TablePartial", model);
        }

        [Authorize(Permissions.Products.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _productsBiz.DeleteAsync(id, user.Id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Products.Delete)]
        public async Task<IActionResult> Restore(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productsBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
