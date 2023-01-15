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
    public class VisitReasonsController : BaseController
    {
        private readonly VisitReasonsBiz _visitReasonsBiz;

        public VisitReasonsController(VisitReasonsBiz visitReasonsBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _visitReasonsBiz = visitReasonsBiz;
        }
        [Authorize(Permissions.VisitReasons.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetVisitReasons()
        {
            List<VisitReasonViewModel> VisitReasons = _mapper.Map<List<VisitReasonViewModel>>(await _visitReasonsBiz.GetAllAsync());
            return PartialView("_ShowVisitReasonsPartial", VisitReasons);
        }

        [Authorize(Permissions.VisitReasons.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.VisitReasons.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(VisitReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                VisitReason type = _mapper.Map<VisitReason>(model);
                bool result = await _visitReasonsBiz.AddAsync(type, user.Id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [Authorize(Permissions.VisitReasons.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<VisitReasonViewModel>(await _visitReasonsBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.VisitReasons.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(VisitReasonViewModel model)
        {
            var visitReason = _mapper.Map<VisitReason>(await _visitReasonsBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            visitReason.Name = model.Name;
            visitReason.NameAr = model.NameAr;
            bool result = await _visitReasonsBiz.UpdateAsync(visitReason, user.Id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.VisitReasons.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _visitReasonsBiz.ArchiveAsync(id, userId);
            return Json(result);
        }
        [Authorize(Permissions.VisitReasons.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            var result = await _visitReasonsBiz.UnarchiveAsync(id, userId);
            return Json(result);
        }
    }
}
