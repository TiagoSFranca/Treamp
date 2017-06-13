using System.Collections.Generic;

namespace Presentation.Models.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdState { get; set; }
        public StateViewItem State { get; set; }
    }

    public class CityViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}