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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var cost = new CostViewModel();
            cost.IdTravel = idTravel;
            return View("_AddMyCost", cost);
        }

        [HttpPost]
        public ActionResult AddMyCost(CostViewModel cost)
        {
            if (VerifyCostIsEmpty(cost.Price))
                ModelState.AddModelError("Price", "Custo não pode ser zero.");
            if (!ModelState.IsValid)
            {
                return View("_AddMyCost", cost);
            }
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

        public ActionResult AddGroupCost(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var cost = new CostViewCreate();
            cost.IdTravel = idTravel;
            cost.Users = FulFillLists(idTravel);
            return View("_AddGroupCost", cost);
        }

        [HttpPost]
        public ActionResult AddGroupCost(CostViewCreate cost)
        {
            if (VerifyCostIsEmpty(cost.Price))
                ModelState.AddModelError("Price", "Custo não pode ser zero.");
            if (!ModelState.IsValid)
            {
                cost.Users = FulFillLists(cost.IdTravel);
                return View("_AddGroupCost", cost);
            }
            messageModel.Title = "Adicionar Custo Pessoal";
            if (VerifyTravelContainsUser(cost.IdTravel))
            {
                var costMapped = AutoMapper.Mapper.Map<CostViewCreate, Cost>(cost);
                costMapped.IdTypeCost = ((int)TypeCostEnum.GROUP);
                costMapped.CreatedDate = DateTime.Now.Date;
                try
                {
                    db.Cost.Add(costMapped);
                    foreach (var item in cost.UserListSelected)
                    {
                        TravelUserCost travelUserCost = new TravelUserCost()
                        {
                            IdCost = costMapped.Id,
                            IdTravelUserTravel = cost.IdTravel,
                            IdTravelUserUser = item
                        };
                        db.TravelUserCost.Add(travelUserCost);
                    }
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

        public ActionResult SeeGroupCost(int idCost)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            GroupViewModel group = new GroupViewModel();
            var Users = db.TravelUserCost.Where(c => c.IdCost == idCost).Select(t => t.TravelUser).Select(c => c.User).ToList();
            group.Members = AutoMapper.Mapper.Map<List<User>, List<UserViewItem>>(Users);
            return View("_SeeGroupCost", group);
        }

        private List<UserViewItem> FulFillLists(int idTravel)
        {
            var Users = db.TravelUser.Where(t => t.IdTravel == idTravel).Select(t => t.User).ToList();
            return AutoMapper.Mapper.Map<List<User>, List<UserViewItem>>(Users);
        }

        private bool VerifyTravelContainsUser(int idTravel)
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var result = db.TravelUser.FirstOrDefault(t => t.IdTravel == idTravel && t.IdUser == userLogged.Id);
            if (result != null)
                return true;
            return false;
        }

        private bool VerifyCostIsEmpty(decimal cost)
        {
            return (cost == 0);
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

        public static List<Cost> GetTravelCost(int idTravel)
        {
            var Costs = db.TravelUserCost.Where(
                tu => tu.IdTravelUserTravel == idTravel
                && tu.Cost.TypeCost.Id == ((int)TypeCostEnum.GROUP)
            ).Select(tu => tu.Cost).OrderBy(t => t.CreatedDate).ToList();
            return Costs;
        }
    }
}