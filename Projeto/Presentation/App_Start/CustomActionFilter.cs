using Presentation.Models;
using Presentation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Presentation.App_Start
{
    public class CustomActionFilter : ActionFilterAttribute
    {

        PresentationContext db = new PresentationContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (UserViewItem)HttpContext.Current.Session["user"];
            if (user == null)
            {
                if (filterContext.RouteData.Values["action"].ToString().ToLower() != "login")
                {
                    if (filterContext.RouteData.Values["action"].ToString().ToLower() != "register")
                    {
                        if (filterContext.RouteData.Values["action"].ToString().ToLower() != "getcities")
                        {

                            filterContext.Result = new RedirectToRouteResult(
                                           new RouteValueDictionary
                                           {
                                       { "action", "Login" },
                                       { "controller", "User" }
                                           });
                        }
                    }
                }
            }
            if (user != null){
                Verify();
            }
            base.OnActionExecuting(filterContext);
        }

        private void Verify()
        {
            DateTime currentDate = DateTime.Now.Date;
            var data = db.Travel.ToList();
            //var data = db.Travel.ToList().SelectMany(t => t.Destinations).Select(c => c.Date >= currentDate ).ToList();
            var Travels = AutoMapper.Mapper.Map<List<Travel>, List<TravelViewModel>>(data);
            var currentTravels = Travels.Where(t => t.Destinations[0].Date <= currentDate && t.Destinations[1].Date >= currentDate).ToList();
            var travelsToFinsh = Travels.Where(t => t.Destinations[1].Date < currentDate && !t.Pendent && !t.Finished).ToList();
            if (currentTravels.Count > 0)
            {
                var currentPendents = currentTravels.Where(t => t.Pendent).ToList();
                FinishPendent(currentPendents);
            }
            if (travelsToFinsh.Count > 0)
            {
                FinishNotFinished(travelsToFinsh);
            }
        }

        private void FinishPendent(List<TravelViewModel> Travels)
        {
            if (Travels.Count > 0)
            {
                foreach (var item in Travels)
                {
                    var travelUsers = db.TravelUser.Where(t => t.IdTravel == item.Id && t.Pendent).ToList();
                    db.TravelUser.RemoveRange(travelUsers);
                    var travel = db.Travel.Find(item.Id);
                    travel.Pendent = false;
                }
                db.SaveChanges();
            }
        }

        private void FinishNotFinished(List<TravelViewModel> Travels)
        {
            if (Travels.Count > 0)
            {
                foreach (var item in Travels)
                {
                    var travel = db.Travel.Find(item.Id);
                    travel.Finished = true;
                }
                db.SaveChanges();
            }
        }
    }
}