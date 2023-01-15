using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class QuotationOffersBiz : BizBaseT<QuotationOffer>
    {
        private readonly IQuotationOffersRepository _repository;
        public QuotationOffersBiz(IQuotationOffersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<QuotationOffer>> GetByQuotationId(int quotationId)
        {
            return await _repository.GetFullItemsByQuotationId(quotationId);
        }

        public override async Task<QuotationOffer> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public async Task<string> GenerateNewcode(string departmentCode)
        {
            string codePrefix = $"{departmentCode}-{DateTime.UtcNow:yyMMdd}";

            var offerIndices = (await Repository
                .GetAllAsync(o => o.Code.StartsWith(codePrefix)))
                .Select(x => x.Code[codePrefix.Length..])
                .Select(str => int.Parse(str))
                .ToArray();

            int nextCode = offerIndices.Length > 0 ? offerIndices.Max() + 1 : 1;
            return $"{codePrefix}{nextCode:00}";
        }
    }
}
