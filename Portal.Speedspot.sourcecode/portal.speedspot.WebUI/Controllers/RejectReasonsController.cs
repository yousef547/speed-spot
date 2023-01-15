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
    public class RejectReasonsController : BaseController
    {
        private readonly RejectReasonsBiz _rejectReasonsBiz;

        public RejectReasonsController(RejectReasonsBiz rejectReasonsBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _rejectReasonsBiz = rejectReasonsBiz;
        }

        [Authorize(Permissions.RejectReasons.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetRejectReasons()
        {
            List<RejectReasonViewModel> RejectReasons = _mapper.Map<List<RejectReasonViewModel>>(await _rejectReasonsBiz.GetAllAsync());
            return PartialView("_ShowRejectReasonPartial", RejectReasons);
        }

        [Authorize(Permissions.RejectReasons.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Permissions.RejectReasons.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(RejectReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                RejectReason type = _mapper.Map<RejectReason>(model);
                bool result = await _rejectReasonsBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [Authorize(Permissions.RejectReasons.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<RejectReasonViewModel>(await _rejectReasonsBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize(Permissions.RejectReasons.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(RejectReasonViewModel model)
        {
            var rejectReason = _mapper.Map<RejectReason>(await _rejectReasonsBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            rejectReason.Name = model.Name;
            rejectReason.NameAr = model.NameAr;
            bool result = await _rejectReasonsBiz.UpdateAsync(rejectReason);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Permissions.RejectReasons.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _rejectReasonsBiz.ArchiveAsync(id, userId);
            return Json(result);
        }

        [Authorize(Permissions.RejectReasons.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _rejectReasonsBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
