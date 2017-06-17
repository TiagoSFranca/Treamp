using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Banco")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Agência")]
        public string Agency { get; set; }
        [Required]
        [Display(Name = "Número")]
        public string Account { get; set; }
        public int IdUser { get; set; }
        [Required]
        [Display(Name = "Tipo de Conta")]
        public int IdTypeAccount { get; set; }
        public TypeAccountViewItem TypeAccount { get; set; }
        public UserViewItem User { get; set; }
        public List<TypeAccountViewItem> TypeAccountList { get; set; }
    }

    public class BankAccountViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
    }
}