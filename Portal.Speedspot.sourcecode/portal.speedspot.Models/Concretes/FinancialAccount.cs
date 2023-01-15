using Microsoft.EntityFrameworkCore.Infrastructure;
using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class FinancialAccount : EntityBase
    {
        private FinancialAccount _parent;
        private IList<FinancialAccount> _childs;

        public FinancialAccount()
        {
        }

        private FinancialAccount(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        public int? ParentId { get; set; }
        public FinancialAccount Parent
        {
            get => LazyLoader.Load(this, ref _parent);
            set => _parent = value;
        }
        [Required]
        public string Code { get; set; }
        [Required]
        public string AutoCode { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public FinancialAccountType Type { get; set; }

        public IList<FinancialAccount> Childs
        {
            get => LazyLoader.Load(this, ref _childs);
            set => _childs = value;
        }
    }
}
