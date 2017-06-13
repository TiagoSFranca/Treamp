using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ViewModels
{
    public class DestinationViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int IdTravel { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public int IdCity { get; set; }

        public CityViewModel City { get; set; }

        public TravelViewItem Travel { get; set; }
    }

    public class DestinationViewItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}