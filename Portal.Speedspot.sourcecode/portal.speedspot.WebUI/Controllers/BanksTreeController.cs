using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.WebUI.Controllers
{
    public class BanksTreeController : BaseController
    {
        private readonly BanksBiz _banksBiz;
        private readonly CountriesBiz _countriesBiz;
        public BanksTreeController(BanksBiz banksBiz,
            CountriesBiz countriesBiz,
            LocalizationService localizationService,
            IMapper mapper,
            ApplicationUserManager userManager) : base(mapper, userManager, localizationService)
        {
            _banksBiz = banksBiz;
            _countriesBiz = countriesBiz;
        }

        public async Task<List<Bankgrouping>> LoadBankTree()
        {
            var banks = _mapper.Map<List<BankViewModel>>(await _banksBiz.GetAllUnarchivedAsync());
            var bankGroups = banks.GroupBy(g => new { g.CountryId, g.CountryName, g.CountryNameAr }).Select(x => new Bankgrouping
            {
                CountryId = x.Key.CountryId,
                CountryName = x.Key.CountryName,
                CountryNameAr = x.Key.CountryNameAr,
                BankVMs = x.ToList()
            }).ToList();

            ViewData["Countries"] = await _countriesBiz.GetAllUnarchivedExceptAsync(banks.Select(b => b.CountryId).Distinct().ToList());
            return bankGroups;
        }

        [Authorize(Permissions.Banks.View)]
        public async Task<IActionResult> Index()
        {
            var bankGroups = await LoadBankTree();
            return View(bankGroups);
        }

        [Authorize(Permissions.Banks.Create)]
        public async Task<IActionResult> CreateBank(BankViewModel model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            Bank bank = _mapper.Map<Bank>(model);
            bank.BankFees = new BankFees
            {
                BidBondMinFees= model.BidBondMinFees,
                BidBondPercentage = model.BidBondPercentage,
                BidBondCreditPercentage = model.BidBondCreditPercentage,
                PerformanceLGMinFees = model.PerformanceLGMinFees,
                PerformanceLGPercentage = model.PerformanceLGPercentage,
                PerformanceLGCreditPercentage = model.PerformanceLGCreditPercentage,
                FinalLGMinFees = model.FinalLGMinFees,
                FinalLGPercentage = model.FinalLGPercentage,
                FinalLGCreditPercentage = model.FinalLGCreditPercentage,
                ChequeCollectionMinFees = model.ChequeCollectionMinFees,
                ChequeCollectionPercentage = model.ChequeCollectionPercentage,
                ChequeCollectionCreditPercentage = model.ChequeCollectionCreditPercentage,
                TransferMinFees = model.TransferMinFees,
                TransferPercentage = model.TransferPercentage,
                TransferCreditPercentage = model.TransferCreditPercentage,
                LCMinFees = model.LCMinFees,
                LCPercentage = model.LCPercentage,
                LCCreditPercentage = model.LCCreditPercentage,
                Form4MinFees = model.Form4MinFees,
                Form4Percentage = model.Form4Percentage,
                Form4CreditPercentage = model.Form4CreditPercentage,
                Form5MinFees = model.Form5MinFees,
                Form5Percentage = model.Form5Percentage,
                Form5CreditPercentage = model.Form5CreditPercentage,
                ForeignExchangeMinFees = model.ForginExchangeMinFees,
                ForeignExchangePercentage = model.ForginExchangePercentage,
                ForeignExchangeCreditPercentage = model.ForginExchangeCreditPercentage,
            };

            await _banksBiz.AddAsync(bank, userId);
            var bankGroups = await LoadBankTree();
            return PartialView("_TreeDataPartial", bankGroups);
        }

        [Authorize(Permissions.Banks.Delete)]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _banksBiz.ArchiveAsync(id, userId);

            var bankGroups = await LoadBankTree();
            return PartialView("_TreeDataPartial", bankGroups);
        }

        [Authorize(Permissions.Banks.Edit)]
        public async Task<IActionResult> GetBankInfo(int id)
        {
            var model = _mapper.Map<BankViewModel>(await _banksBiz.GetByIdAsync(id));

            return PartialView("_BankInfoModal", model);
        }

        [Authorize(Permissions.Banks.Edit)]
        public async Task<IActionResult> UpdateBank(BankViewModel model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            Bank bank = _mapper.Map<Bank>(model);
            bank.BankFees = new BankFees
            {
                Id = model.BankFeesId,
                BidBondMinFees = model.BidBondMinFees,
                BidBondPercentage = model.BidBondPercentage,
                BidBondCreditPercentage = model.BidBondCreditPercentage,
                PerformanceLGMinFees = model.PerformanceLGMinFees,
                PerformanceLGPercentage = model.PerformanceLGPercentage,
                PerformanceLGCreditPercentage = model.PerformanceLGCreditPercentage,
                FinalLGMinFees = model.FinalLGMinFees,
                FinalLGPercentage = model.FinalLGPercentage,
                FinalLGCreditPercentage = model.FinalLGCreditPercentage,
                ChequeCollectionMinFees = model.ChequeCollectionMinFees,
                ChequeCollectionPercentage = model.ChequeCollectionPercentage,
                ChequeCollectionCreditPercentage = model.ChequeCollectionCreditPercentage,
                TransferMinFees = model.TransferMinFees,
                TransferPercentage = model.TransferPercentage,
                TransferCreditPercentage = model.TransferCreditPercentage,
                LCMinFees = model.LCMinFees,
                LCPercentage = model.LCPercentage,
                LCCreditPercentage = model.LCCreditPercentage,
                Form4MinFees = model.Form4MinFees,
                Form4Percentage = model.Form4Percentage,
                Form4CreditPercentage = model.Form4CreditPercentage,
                Form5MinFees = model.Form5MinFees,
                Form5Percentage = model.Form5Percentage,
                Form5CreditPercentage = model.Form5CreditPercentage,
                ForeignExchangeMinFees = model.ForginExchangeMinFees,
                ForeignExchangePercentage = model.ForginExchangePercentage,
                ForeignExchangeCreditPercentage = model.ForginExchangeCreditPercentage,
            };

            await _banksBiz.UpdateAsync(bank, userId);

            var bankGroups = await LoadBankTree();
            return PartialView("_TreeDataPartial", bankGroups);
        }

        [Authorize(Permissions.Banks.Create)]
        public async Task<IActionResult> CreateBranch(BankBranchViewModel model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            BankBranch branch = _mapper.Map<BankBranch>(model);
            await _banksBiz.AddBranchAsync(branch, userId);

            var bankGroups = await LoadBankTree();
            return PartialView("_TreeDataPartial", bankGroups);
        }

        [Authorize(Permissions.Banks.Delete)]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            await _banksBiz.ArchiveBranchAsync(branchId, userId);

            var bankGroups = await LoadBankTree();
            return PartialView("_TreeDataPartial", bankGroups);
        }

        [Authorize(Permissions.Banks.Edit)]
        public async Task<IActionResult> GetBranchInfo(int branchId)
        {
            var model = _mapper.Map<BankBranchViewModel>(await _banksBiz.GetBranchAsync(branchId));

            return PartialView("_BranchInfoModal", model);
        }

        [Authorize(Permissions.Banks.Edit)]
        public async Task<IActionResult> UpdateBranch(BankBranchViewModel model)
        {
            var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            BankBranch branch = _mapper.Map<BankBranch>(model);
            await _banksBiz.UpdateBranchAsync(branch, userId);

            var bankGroups = await LoadBankTree();
            return PartialView("_TreeDataPartial", bankGroups);
        }
    }
}
