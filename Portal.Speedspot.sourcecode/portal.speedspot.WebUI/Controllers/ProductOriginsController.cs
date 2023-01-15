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
    public class ProductOrigins : BaseController
    {
        private readonly ProductOriginsBiz _productOriginsBiz;

        public ProductOrigins(ProductOriginsBiz productOriginsBiz,
            IMapper mapper,
            ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _productOriginsBiz = productOriginsBiz;
        }

        [Authorize(Permissions.ProductOrigins.View)]
        public async Task<IActionResult> Index()
        {
            var productOrigins = await _productOriginsBiz.GetAllAsync();
            return View(productOrigins);
        }

        [Authorize(Permissions.ProductOrigins.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.ProductOrigins.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductOriginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var productOrigin = _mapper.Map<ProductOrigin>(model);
                var result = await _productOriginsBiz.AddAsync(productOrigin, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.ProductOrigins.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<ProductOriginViewModel>(await _productOriginsBiz.GetByIdAsync(id));
            return View(model);
        }

        [Authorize(Permissions.ProductOrigins.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductOriginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                var productOrigin = _mapper.Map<ProductOrigin>(model);
                var result = await _productOriginsBiz.UpdateAsync(productOrigin, userId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize(Permissions.ProductOrigins.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productOriginsBiz.ArchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.ProductOrigins.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _productOriginsBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
