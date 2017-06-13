using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Presentation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );


           // --INSERT INTO TYPE ACCOUNT

           //INSERT INTO TypeAccount(Name)values('Conta Poupança');
           // INSERT INTO TypeAccount(Name) values('Conta Corrente');

           // --INSERT INTO TYPE COST

           //INSERT INTO TypeCost(Name)values('Pessoal');
           // INSERT INTO TypeCost(Name) values('Grupo');
        }
    }
}
