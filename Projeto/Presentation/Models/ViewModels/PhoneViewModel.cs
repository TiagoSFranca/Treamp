namespace Presentation.Models.ViewModels
{
    public class PhoneViewModel
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string Number { get; set; }
        public int IdUser { get; set; }
        public virtual User User { get; set; }
    }

    public class PhoneViewItem
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string Number { get; set; }
    }
}