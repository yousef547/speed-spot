using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompanyData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DescriptionAr { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string DefaultOfferNotes { get; set; }
        [Required]
        public string DefaultOfferNotesAr { get; set; }

        [Required]
        public string DefaultOfferTechnicalNotes { get; set; }
        [Required]
        public string DefaultOfferTechnicalNotesAr { get; set; }

        [Required]
        public string Address { get; set; }

        public string Email { get; set; }
        public string Website { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
    }
}
