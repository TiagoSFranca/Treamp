using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class TravelUserCostViewModel
    {
        public int IdTravelUserTravel { get; set; }
        public int IdTravelUserUser { get; set; }
        public int IdCost { get; set; }
        public CostViewItem Cost { get; set; }
        public TravelUserViewItem TravelUser { get; set; }
    }


    public class TravelUserCostViewItem
    {
        public int IdTravelUserTravel { get; set; }
        public int IdTravelUserUser { get; set; }
        public int IdCost { get; set; }
        //public CostViewItem Cost { get; set; }
        //public TravelUserViewItem TravelUser { get; set; }
    }
}