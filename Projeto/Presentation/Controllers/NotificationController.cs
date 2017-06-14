using Presentation.Models;
using Presentation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class NotificationController : Controller
    {
        private static PresentationContext db = new PresentationContext();
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
        
        public static void Create(int IdUser, string message)
        {
            NotificationViewModel notification = new NotificationViewModel();
            notification.IdUser = IdUser;
            notification.Active = true;
            notification.Description = message;
            notification.Date = DateTime.Now.Date;
            var notificationMapped = AutoMapper.Mapper.Map<NotificationViewModel, Notification>(notification);
            try
            {
                db.Notification.Add(notificationMapped);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // GET: Notification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notification/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
