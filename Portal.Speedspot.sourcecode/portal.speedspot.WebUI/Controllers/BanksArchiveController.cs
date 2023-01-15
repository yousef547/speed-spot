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
    public class BanksArchiveController : BaseController
    {
        private readonly BanksBiz _banksBiz;

        public BanksArchiveController(BanksBiz banksBiz,
            CountriesBiz countriesBiz,
            IMapper mapper,
            ApplicationUserManager userManager) : base(mapper, userManager)
        {
            _banksBiz = banksBiz;
        }

        [Authorize(Permissions.Banks.ViewArchived)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadBanks()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Bank> banks = (await _banksBiz.GetAllArchivedAsync()).ToList();

            List<BankViewModel> model = _mapper.Map<List<BankViewModel>>(banks);

            return PartialView("_TablePartial", model);
        }

        [Authorize(Permissions.Banks.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _banksBiz.DeleteAsync(id, user.Id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Banks.Delete)]
        public async Task<IActionResult> Restore(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _banksBiz.UnarchiveAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
