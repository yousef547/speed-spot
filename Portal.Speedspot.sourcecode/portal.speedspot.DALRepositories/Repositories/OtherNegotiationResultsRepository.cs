﻿using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OtherNegotiationResultsRepository : RepositoryBase<OtherNegotiationResult>, IOtherNegotiationResultsRepository
    {
        public OtherNegotiationResultsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}