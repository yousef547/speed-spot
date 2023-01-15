﻿using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class GeneralRequestsRepository : RepositoryBase<GeneralRequest>, IGeneralRequestsRepository
    {
        public GeneralRequestsRepository(ApplicationDbContext context) :base(context)
        {

        }
    }
}
