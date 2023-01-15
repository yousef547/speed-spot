﻿using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class QuotationCurrenciesRepository : RepositoryBase<QuotationCurrency>, IQuotationCurrenciesRepository
    {
        public QuotationCurrenciesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
