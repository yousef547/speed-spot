using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class PaymentTypesController : BaseController
    {
        private readonly PaymentTypesBiz _paymentTypesBiz;

        public PaymentTypesController(PaymentTypesBiz paymentTypesBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _paymentTypesBiz = paymentTypesBiz;
        }

        [Authorize(Permissions.PaymentTypes.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetPaymentTypes()
        {
            List<PaymentTypeViewModel> PaymentTypes = _mapper.Map<List<PaymentTypeViewModel>>(await _paymentTypesBiz.GetAllAsync());
            return PartialView("_ShowPaymentTypePartial", PaymentTypes);
        }

        [Authorize(Permissions.PaymentTypes.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.PaymentTypes.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(PaymentTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                PaymentType type = _mapper.Map<PaymentType>(model);
                bool result = await _paymentTypesBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                } 
            }
            return View(model);
        }
        [Authorize(Permissions.PaymentTypes.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<PaymentTypeViewModel>(await _paymentTypesBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.PaymentTypes.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(PaymentTypeViewModel model)
        {
            var paymentType = _mapper.Map<PaymentType>(await _paymentTypesBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            paymentType.Name = model.Name;
            paymentType.NameAr = model.NameAr;
            bool result = await _paymentTypesBiz.UpdateAsync(paymentType);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.PaymentTypes.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _paymentTypesBiz.ArchiveAsync(id, userId);
            return Json(result);
        }
        [Authorize(Permissions.PaymentTypes.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
           var result = await _paymentTypesBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
