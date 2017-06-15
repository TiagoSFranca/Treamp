using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class TravelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Pendent { get; set; }
        public bool Finished { get; set; }
        public List<DestinationViewModel> Destinations { get; set; }
        public List<TravelUserViewItem> TravelUsers { get; set; }
        public List<CostViewModel> PersonalCosts { get; set; }
        public List<TravelUserCostViewModel> GroupCosts { get; set; }
    }

    public class TravelViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Pendent { get; set; }
        public bool Finished { get; set; }
    }

    public class TravelViewCreate
    {
        [Required]
        [Display(Name ="Nome")]
        [StringLength(45, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Origem")]
        public DestinationViewModel Origin { get; set; }
        [Required]
        [Display(Name = "Destino")]
        public DestinationViewModel Destination { get; set; }
        public List<StateViewItem> States { get; set; }
        public List<CityViewItem> Cities { get; set; }
    }

    public class TravelViewList
    {
        public List<TravelViewModel> Travels { get; set; }
    }

    public class GroupViewModel
    {
        public List<UserViewItem> Members { get; set; }
    }


}