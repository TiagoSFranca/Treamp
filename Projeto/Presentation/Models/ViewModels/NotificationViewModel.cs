using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int IdUser { get; set; }
        public DateTime Date { get; set; }
        public UserViewItem User { get; set; }

    }

    public class NotificationViewItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}