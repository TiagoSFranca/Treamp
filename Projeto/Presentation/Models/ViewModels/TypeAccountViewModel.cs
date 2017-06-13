using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class TypeAccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BankAccountViewItem> BankAccounts { get; set; }
    }

    public class TypeAccountViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}