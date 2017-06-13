using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public bool Pendent { get; set; }
        public int IdRequester { get; set; }
        public int IdRemittee { get; set; }
        public virtual UserViewItem Requester { get; set; }
        public virtual UserViewItem Remittee { get; set; }
    }

    public class ContactViewItem
    {
        public int Id { get; set; }
        public bool Pendent { get; set; }
        public int IdRequester { get; set; }
        public int IdRemittee { get; set; }
    }

    public class ContactViewList
    {
        public List<ContactViewModel> Contacts { get; set; }
    }

    public class ContactAddOrDelete
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class AddContact
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string Email { get; set; }
    }
}