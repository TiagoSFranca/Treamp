using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class CostViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Custo")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IdTypeCost { get; set; }
        public TypeCostViewItem TypeCost { get; set; }
        public int IdTravel { get; set; }
        public List<TravelUserCostViewItem> TravelUserCost { get; set; }
    }

    public class CostViewItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CostViewCreate
    {
        [Required]
        [Display(Name = "Custo")]
        public decimal Price { get; set; }

        public List<UserViewItem> Users { get; set; }

        [Required(ErrorMessage = "Selecione ao menos 1 integrante.")]
        [Display(Name = "Custo")]
        public List<int> UserListSelected { get; set; }

        public int IdTravel { get; set; }
    }
}