using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class TravelUserViewModel
    {
        public int IdTravel { get; set; }
        public int IdUser { get; set; }
        public TravelViewModel Travel { get; set; }
        //public virtual ICollection<TravelUserCost> TravelUserCost { get; set; }
        public UserViewItem User { get; set; }
    }

    public class TravelUserViewItem
    {
        public int IdTravel { get; set; }
        public int IdUser { get; set; }
        //public TravelViewItem Travel { get; set; }
        //public List<TravelUserCostViewItem> TravelUserCosts { get; set; }
        //public UserViewItem User { get; set; }
    }
    public class TravelUserViewCreate
    {
        public int IdTravel { get; set; }
        [Required]
        [Display(Name = "Contato")]
        public int IdUser { get; set; }
        public List<UserViewItem> Users { get; set; }
    }

    public class TravelUserViewIndex
    {
        public List<TravelUserViewModel> TravelUsers { get; set; }
    }
}