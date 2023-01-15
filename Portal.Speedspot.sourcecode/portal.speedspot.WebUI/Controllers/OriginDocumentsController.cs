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
    public class OriginDocumentsController : BaseController
    {
        private readonly OriginDocumentsBiz _originDocumentsBiz;

        public OriginDocumentsController(OriginDocumentsBiz originDocumentsBiz, IMapper mapper, ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _originDocumentsBiz = originDocumentsBiz;
        }
        [Authorize(Permissions.OriginDocuments.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOriginDocument()
        {
            List<OriginDocumentViewModel> OriginDocument = _mapper.Map<List<OriginDocumentViewModel>>(await _originDocumentsBiz.GetAllAsync());
            return PartialView("_ShowOriginDocumentPartial", OriginDocument);
        }

        [Authorize(Permissions.OriginDocuments.Create)]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Permissions.OriginDocuments.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(OriginDocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                OriginDocument type = _mapper.Map<OriginDocument>(model);
                bool result = await _originDocumentsBiz.AddAsync(type);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [Authorize(Permissions.OriginDocuments.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<OriginDocumentViewModel>(await _originDocumentsBiz.GetByIdAsync(id));
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize(Permissions.OriginDocuments.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit(OriginDocumentViewModel model)
        {
            var origin_Document = _mapper.Map<OriginDocument>(await _originDocumentsBiz.GetByIdAsync(model.Id));
            if (model == null)
            {
                return NotFound();
            }
            origin_Document.Name = model.Name;
            origin_Document.NameAr = model.NameAr;
            bool result = await _originDocumentsBiz.UpdateAsync(origin_Document);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Permissions.OriginDocuments.Delete)]
        public async Task<IActionResult> Archive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            await _originDocumentsBiz.ArchiveAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Permissions.OriginDocuments.Delete)]
        public async Task<IActionResult> Unarchive(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            if (userId == null)
            {
                return NotFound();
            }
            await _originDocumentsBiz.UnarchiveAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
