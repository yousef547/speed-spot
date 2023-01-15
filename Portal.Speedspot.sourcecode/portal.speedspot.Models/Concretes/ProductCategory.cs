using Microsoft.EntityFrameworkCore.Infrastructure;
using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class ProductCategory : EntityBase
    {
        private ProductCategory _parent;
        private IList<ProductCategory> _childs;
        private IList<Product> _products;

        public ProductCategory()
        {
        }

        private ProductCategory(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }

        public bool IsService { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? ParentId { get; set; }
        public ProductCategory Parent
        {
            get => LazyLoader.Load(this, ref _parent);
            set => _parent = value;
        }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string AutoCode { get; set; }

        public IList<ProductCategory> Childs
        {
            get => LazyLoader.Load(this, ref _childs);
            set => _childs = value;
        }
        public IList<Product> Products
        {
            get => LazyLoader.Load(this, ref _products);
            set => _products = value;
        }
    }
}
