using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;

namespace portal.speedspot.WebUI.Controllers
{
    public class ArchivesController : BaseController
    {
        private readonly OpportunitiesBiz _opportunitiesBiz;

        public ArchivesController(OpportunitiesBiz opportunitiesBiz,
            ApplicationUserManager userManager) : base(userManager)
        {
            _opportunitiesBiz = opportunitiesBiz;
        }

        [Authorize(Permissions.Archives.View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Opportunities.Delete)]
        public async Task<IActionResult> ArchiveOpportunities(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _opportunitiesBiz.ArchiveAsync(id, userId);

            var opportunity = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetBasicDataByIdAsync(id));
            await SaveUserActivity(userId, UserActivities.Opportunities.Archive, opportunity.Name);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Opportunities.Delete)]
        public async Task<IActionResult> UnarchiveOpportunities(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _opportunitiesBiz.UnarchiveAsync(id, userId);

            var opportunity = _mapper.Map<OpportunityViewModel>(await _opportunitiesBiz.GetBasicDataByIdAsync(id));
            await SaveUserActivity(userId, UserActivities.Opportunities.Unarchive, opportunity.Name);


            return RedirectToAction(nameof(Index));
        }
    }
}
