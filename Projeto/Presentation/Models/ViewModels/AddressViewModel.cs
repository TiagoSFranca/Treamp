using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Rua")]
        public string Street { get; set; }
        [Display(Name = "Bairro")]
        public string District { get; set; }
        [Display(Name = "Número")]
        public string Number { get; set; }
        public int IdCity { get; set; }
        public int IdUser { get; set; }
        public CityViewModel City { get; set; }
        public UserViewItem User { get; set; }
    }

    public class AddressViewItem
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Number { get; set; }
    }

    public class AddressViewRegister
    {
        [Required]
        [Display(Name = "Rua")]
        [StringLength(100, ErrorMessage = "Deve possuir no mínimo {2} letras", MinimumLength = 2)]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        [StringLength(45, ErrorMessage = "Deve possuir no mínimo {2} letras", MinimumLength = 2)]
        public string District { get; set; }

        [Required]
        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "Deve possuir no mínimo {2} letras", MinimumLength = 1)]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public int IdCity { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int IdState { get; set; }


        public List<StateViewItem> States { get; set; }
        public List<CityViewItem> Cities { get; set; }
    }
}