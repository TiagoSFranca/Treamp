﻿using Presentation.Models;
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
        UserViewItem userLogged;

        public ActionResult Index()
        {

            userLogged = (UserViewItem)HttpContext.Session["user"];
            var result = db.Notification.Where(t => t.Active && t.IdUser == userLogged.Id).OrderBy(c => c.Date).ToList();
            result.ForEach(x => { x.Active = false; });
            db.SaveChanges();
            List<NotificationViewModel> Notifications = AutoMapper.Mapper.Map<List<Notification>, List<NotificationViewModel>>(result);
            return View(Notifications);
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

        public int GetNumberPendentNotifications()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var result = db.Notification.Where(t => t.Active && t.IdUser == userLogged.Id).Count();
            return result;
        }
    }
}
