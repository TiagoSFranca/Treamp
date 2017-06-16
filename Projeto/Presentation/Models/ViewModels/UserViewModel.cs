using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDay { get; set; }
        public List<AddressViewItem> Addresses { get; set; }
        public List<BankAccountViewItem> BankAccounts { get; set; }
        public List<ContactViewItem> Requests { get; set; }
        public List<ContactViewItem> Remittees { get; set; }
        public List<NotificationViewItem> Notifications { get; set; }
        public List<PhoneViewItem> Phones { get; set; }
        public List<TravelUserViewItem> TravelUsers { get; set; }
    }

    public class UserViewItem
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    public class UserViewLogin
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 5)]
        public string Login { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserViewRegister
    {
        public UserViewRegister()
        {
            Address = new AddressViewRegister();
        }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(45, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        [StringLength(45, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 2)]
        public string LastName { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [StringLength(20, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "Senhas diferentes.")]
        [StringLength(20, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public AddressViewRegister Address { get; set; }
    }

    public class UserViewEdit
    {
        [Required]
        [Display(Name = "Nome")]
        [StringLength(45, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        [StringLength(45, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [StringLength(20, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 6)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [StringLength(20, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("NewPassword", ErrorMessage = "Senhas diferentes.")]
        [StringLength(20, ErrorMessage = "Deve possuir entre {2} e {1} letras", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}