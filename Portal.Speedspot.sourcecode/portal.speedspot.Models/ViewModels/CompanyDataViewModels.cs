using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CompanyDataViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Description))]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DescriptionAr))]
        public string DescriptionAr { get; set; }

        public string ImagePath { get; set; }

        public IFormFile LogoFile { get; set; }

        public bool IsFavourite { get; set; }


        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DefaultOfferNotes))]
        public string DefaultOfferNotes { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DefaultOfferNotesAr))]
        public string DefaultOfferNotesAr { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DefaultOfferTechnicalNotes))]
        public string DefaultOfferTechnicalNotes { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DefaultOfferTechnicalNotesAr))]
        public string DefaultOfferTechnicalNotesAr { get; set; }


        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Address))]
        public string Address { get; set; }

        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Display(Name = nameof(Website))]
        public string Website { get; set; }

        [Display(Name = nameof(Mobile))]
        public string Mobile { get; set; }

        [Display(Name = nameof(Telephone))]
        public string Telephone { get; set; }

        [Display(Name = nameof(Fax))]
        public string Fax { get; set; }
    }
}
