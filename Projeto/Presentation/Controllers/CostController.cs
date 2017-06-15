using Presentation.Models;
using Presentation.Models.ViewModels;
using Presentation.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class CostController : Controller
    {
        private static PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;
        public ActionResult AddMyCost(int idTravel)
        {
            var cost = new CostViewModel();
            cost.IdTravel = idTravel;
            return View("_AddMyCost", cost);
        }

        [HttpPost]
        public ActionResult AddMyCost(CostViewModel cost)
        {
            messageModel.Title = "Adicionar Custo Pessoal";
            if (VerifyTravelContainsUser(cost.IdTravel))
            {
                cost.IdTypeCost = ((int)TypeCostEnum.PERSONAL);
                cost.CreatedDate = DateTime.Now.Date;
                var costMapped = AutoMapper.Mapper.Map<CostViewModel, Cost>(cost);
                try
                {
                    db.Cost.Add(costMapped);
                    TravelUserCost travelUserCost = new TravelUserCost()
                    {
                        IdCost = costMapped.Id,
                        IdTravelUserTravel = cost.IdTravel,
                        IdTravelUserUser = userLogged.Id
                    };
                    db.TravelUserCost.Add(travelUserCost);
                    db.SaveChanges();
                    messageModel.Message = "Custo adicionado com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        private bool VerifyTravelContainsUser(int idTravel)
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var result = db.TravelUser.FirstOrDefault(t => t.IdTravel == idTravel && t.IdUser == userLogged.Id);
            if (result != null)
                return true;
            return false;
        }

        public static List<CostViewModel> GetPersonalCost(int idUser, int idTravel)
        {
            List<CostViewModel> CostsMapped = new List<CostViewModel>();
            var Costs = db.TravelUserCost.Where(
                tu => tu.IdTravelUserTravel == idTravel
                && tu.IdTravelUserUser == idUser
                && tu.Cost.TypeCost.Id == ((int)TypeCostEnum.PERSONAL)
            ).Select(tu => tu.Cost).OrderBy(t => t.CreatedDate).ToList();
            CostsMapped = AutoMapper.Mapper.Map<List<Cost>, List<CostViewModel>>(Costs);
            return CostsMapped;
        }
    }
}