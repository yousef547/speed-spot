﻿using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PaymentRequestsRepository : RepositoryBase<PaymentRequest>, IPaymentRequestsRepository
    {
        public PaymentRequestsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
