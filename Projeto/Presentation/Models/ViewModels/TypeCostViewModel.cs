using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class TypeCostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<CostViewItem> Costs { get; set; }
    }

    public class TypeCostViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}