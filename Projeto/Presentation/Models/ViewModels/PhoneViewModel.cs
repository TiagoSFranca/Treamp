using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ViewModels
{
    public class PhoneViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="DDD")]
        public string Prefix { get; set; }
        [Required]
        [Display(Name = "Número")]
        public string Number { get; set; }
        public int IdUser { get; set; }
        public UserViewItem User { get; set; }
    }

    public class PhoneViewItem
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string Number { get; set; }
    }
}