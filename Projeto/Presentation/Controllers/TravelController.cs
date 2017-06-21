using Presentation.Models;
using Presentation.Models.ViewModels;
using Presentation.Util;
using Presentation.WebServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class TravelController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;
        DateTime currentDate = DateTime.Now.Date;

        //OK
        public async Task<ActionResult> NewTravel()
        {
            TravelViewCreate travel = new TravelViewCreate();
            await FillLists(travel);
            return View(travel);
        }

        //OK
        [HttpPost]
        public async Task<ActionResult> NewTravel(TravelViewCreate travel)
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            if (!ModelState.IsValid)
            {
                await FillLists(travel);
                return View(travel);

            }
            if (!VerifyCanBeAddTravel(travel.Origin, travel.Destination, userLogged))
            {
                ModelState.AddModelError("Name", "Você já possui viagem(s) para esse período");
                await FillLists(travel);
                return View(travel);
            }
            try
            {
                Travel travelMapped = AutoMapper.Mapper.Map<TravelViewCreate, Travel>(travel);
                travelMapped.Pendent = true;
                db.Travel.Add(travelMapped);
                Destination origin = AutoMapper.Mapper.Map<DestinationViewModel, Destination>(travel.Origin);
                origin.IdTravel = travelMapped.Id;
                Destination destination = AutoMapper.Mapper.Map<DestinationViewModel, Destination>(travel.Destination);
                destination.IdTravel = travelMapped.Id;
                db.Destination.Add(origin);
                db.Destination.Add(destination);
                var travelUser = new TravelUser()
                {
                    IdTravel = travelMapped.Id,
                    IdUser = userLogged.Id,
                    IsOwner = true,
                    Pendent = false
                };
                db.TravelUser.Add(travelUser);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", "Ocorreu um erro");
                await FillLists(travel);
                return View(travel);
            }
            return RedirectToAction("Pendent");
        }

        #region Pendent Methods
        //OK
        public ActionResult Pendent()
        {
            return View();
        }

        //OK
        public async Task<ActionResult> MyPendents()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelViewList Travels = new TravelViewList();
            var data = db.TravelUser.Where(t => t.IdUser == userLogged.Id && t.IsOwner && t.Travel.Pendent).Select(t => t.Travel).ToList();
            Travels.Travels = AutoMapper.Mapper.Map<List<Travel>, List<TravelViewModel>>(data);
            if (Travels.Travels != null)
            {
                foreach (var item in Travels.Travels)
                {
                    foreach (var element in item.Destinations)
                    {
                        var city = await CityWS.GetCity(element.IdCity);
                        element.City = city;
                    }
                }
            }
            return PartialView("_MyPendents", Travels);
        }

        //OK
        public async Task<ActionResult> GuestPendents()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelUserViewIndex TravelUser = new TravelUserViewIndex();
            var teste = db.TravelUser.Where(tu => tu.IdUser == userLogged.Id && tu.Pendent && !tu.Travel.Finished).ToList();
            TravelUser.TravelUsers = AutoMapper.Mapper.Map<List<TravelUser>, List<TravelUserViewModel>>(teste);
            if (TravelUser.TravelUsers != null)
            {
                foreach (var item in TravelUser.TravelUsers)
                {
                    foreach (var element in item.Travel.Destinations)
                    {
                        var city = await CityWS.GetCity(element.IdCity);
                        element.City = city;
                    }
                }
            }
            return PartialView("_GuestPendents", TravelUser);
        }

        //OK
        public ActionResult InviteMember(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelUserViewCreate travel = new TravelUserViewCreate();
            travel.IdTravel = idTravel;
            FillListContact(travel, userLogged);
            return View("_InviteMember", travel);
        }

        //OK
        [HttpPost]
        public ActionResult InviteMember(TravelUserViewCreate travel)
        {
            if (!ModelState.IsValid)
            {
                FillListContact(travel, userLogged);
                return View("_InviteMember", travel);
            }
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var travelMapped = AutoMapper.Mapper.Map<TravelUserViewCreate, TravelUser>(travel);
            travelMapped.Pendent = true;
            try
            {
                db.TravelUser.Add(travelMapped);
                db.SaveChanges();
                NotificationController.Create(travel.IdUser, Services.InviteToTravel(userLogged.FullName));
                messageModel.Message = "Solicitação enviada com sucesso!";
                messageModel.Title = "Convidar Contato";
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Email", "Ocorreu um erro!");
                return View("_InviteMember", travel);
            }
            return View("_Message", messageModel);
        }

        //OK
        public ActionResult CloseTravel(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var travel = db.Travel.Where(t => t.Id == idTravel).FirstOrDefault();
            if (travel == null)
            {
                return RedirectToAction("Pendent");
            }
            TravelViewModel travelClose = AutoMapper.Mapper.Map<Travel, TravelViewModel>(travel);
            return View("_CloseTravel", travelClose);
        }

        //OK
        [HttpPost]
        public ActionResult CloseTravel(TravelViewModel travel)
        {

            messageModel.Title = "Finalizar Viagem";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var travelToUpdate = db.Travel.Where(t => t.Id == travel.Id).FirstOrDefault();
            if (travelToUpdate == null)
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            else
            {
                travelToUpdate.Pendent = false;
                try
                {
                    var TravelUsers = db.TravelUser.Where(t => t.IdTravel == travel.Id && t.Pendent).ToList();
                    db.TravelUser.RemoveRange(TravelUsers);
                    db.SaveChanges();
                    messageModel.Message = "Viagem finalizada com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        //OK
        public ActionResult AceptInvite(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var travel = db.TravelUser.Where(t => t.IdTravel == idTravel).FirstOrDefault();
            if (travel == null)
            {
                return RedirectToAction("Pendent");
            }
            TravelViewModel travelAcept = AutoMapper.Mapper.Map<Travel, TravelViewModel>(travel.Travel);
            return View("_AceptInvite", travelAcept);
        }

        //OK
        [HttpPost]
        public ActionResult AceptInvite(TravelViewModel travel)
        {

            messageModel.Title = "Aceitar Convite de Viagem";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var travelToUpdate = db.TravelUser.Where(t => t.IdUser == userLogged.Id && t.IdTravel == travel.Id).FirstOrDefault();
            if (travelToUpdate == null)
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            else
            {
                var travelMapped = AutoMapper.Mapper.Map<TravelUser, TravelUserViewModel>(travelToUpdate);
                if (!VerifyCanBeAddTravel(travelMapped.Travel.Destinations[0], travelMapped.Travel.Destinations[1], userLogged))
                {
                    messageModel.Message = "Você já possui viagem(s) para esse período";
                }
                else
                {
                    travelToUpdate.Pendent = false;
                    try
                    {
                        db.SaveChanges();
                        messageModel.Message = "Convite aceito com sucesso!";
                    }
                    catch (Exception e)
                    {
                        messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                    }
                }
            }
            return View("_Message", messageModel);
        }
        #endregion

        #region Confirmed Methods

        //OK
        public ActionResult Confirmed()
        {
            return View();
        }

        //OK
        public async Task<ActionResult> MyConfirmed()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelViewList Travels = new TravelViewList();
            var data = db.TravelUser.Where(t => t.IdUser == userLogged.Id && !t.Pendent && t.IsOwner && !t.Travel.Finished).Select(t => t.Travel).ToList();
            Travels.Travels = AutoMapper.Mapper.Map<List<Travel>, List<TravelViewModel>>(data);
            if (Travels.Travels != null && Travels.Travels.Count > 0)
            {

                Travels.Travels = Travels.Travels.Where(t => t.Destinations[0].Date >= currentDate && t.Destinations[1].Date <= currentDate).ToList();
                foreach (var item in Travels.Travels)
                {
                    foreach (var element in item.Destinations)
                    {
                        var city = await CityWS.GetCity(element.IdCity);
                        element.City = city;
                    }
                }
            }
            return PartialView("_MyConfirmed", Travels);
        }

        //OK
        public async Task<ActionResult> GuestConfirmed()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelUserViewIndex TravelUser = new TravelUserViewIndex();
            var teste = db.TravelUser.Where(t => t.IdUser == userLogged.Id && !t.Pendent && !t.IsOwner && !t.Travel.Finished).ToList();
            TravelUser.TravelUsers = AutoMapper.Mapper.Map<List<TravelUser>, List<TravelUserViewModel>>(teste);
            if (TravelUser.TravelUsers != null && TravelUser.TravelUsers.Count > 0)
            {

                TravelUser.TravelUsers = TravelUser.TravelUsers.Where(t => t.Travel.Destinations[0].Date >= currentDate && t.Travel.Destinations[1].Date <= currentDate).ToList();
                foreach (var item in TravelUser.TravelUsers)
                {
                    foreach (var element in item.Travel.Destinations)
                    {
                        var city = await CityWS.GetCity(element.IdCity);
                        element.City = city;
                    }
                }
            }
            return PartialView("_GuestConfirmed", TravelUser);
        }
        #endregion

        public async Task<ActionResult> Present()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelViewModel travelViewModel = new TravelViewModel();
            var travel = db.TravelUser.Where(t => t.IdUser == userLogged.Id && !t.Travel.Pendent && !t.Travel.Finished).Select(t => t.Travel).ToList();
            var travelMapped = AutoMapper.Mapper.Map<List<Travel>, List<TravelViewModel>>(travel);
            if (travelMapped.Count > 0)
            {
                travelViewModel = travelMapped.FirstOrDefault(t => t.Destinations[0].Date <= currentDate && t.Destinations[1].Date >= currentDate);
                if (travelViewModel != null && travelViewModel.Destinations != null)
                {
                    foreach (var element in travelViewModel.Destinations)
                    {
                        var city = await CityWS.GetCity(element.IdCity);
                        element.City = city;
                    }
                    travelViewModel.PersonalCosts = CostController.GetPersonalCost(userLogged.Id, travelViewModel.Id);
                    var Costs = CostController.GetTravelCost(travelViewModel.Id);
                    travelViewModel.GroupCosts = AutoMapper.Mapper.Map<List<Cost>, List<CostViewModel>>(Costs);
                    travelViewModel.ValueToPay = CalculateValue(Costs, userLogged.Id, travelViewModel.TravelUsers.Count());
                }
            }
            return View(travelViewModel);
        }

        public async Task<ActionResult> Historic()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            TravelViewList Travels = new TravelViewList();
            var data = db.TravelUser.Where(t => t.IdUser == userLogged.Id && !t.Pendent && t.IsOwner && t.Travel.Finished).Select(t => t.Travel).ToList();
            Travels.Travels = AutoMapper.Mapper.Map<List<Travel>, List<TravelViewModel>>(data);
            if (Travels.Travels != null && Travels.Travels.Count > 0)
            {
                foreach (var item in Travels.Travels)
                {
                    foreach (var element in item.Destinations)
                    {
                        var city = await CityWS.GetCity(element.IdCity);
                        element.City = city;
                    }
                }
            }
            return View(Travels);
        }

        //OK
        public ActionResult SeeGroup(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            GroupViewModel group = new GroupViewModel();
            var TravelUsers = db.TravelUser.Where(tu => tu.IdTravel == idTravel && !tu.Pendent).ToList();
            var Users = TravelUsers.Select(tu => tu.User).ToList();
            group.Members = AutoMapper.Mapper.Map<List<User>, List<UserViewItem>>(Users);
            return View("_SeeGroup", group);
        }

        //OK
        public ActionResult DeleteTravel(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var travel = db.Travel.Where(t => t.Id == idTravel).FirstOrDefault();
            if (travel == null)
            {
                return RedirectToAction("Pendent");
            }
            TravelViewModel travelDelete = AutoMapper.Mapper.Map<Travel, TravelViewModel>(travel);
            return View("_DeleteTravel", travelDelete);
        }

        //OK
        [HttpPost]
        public ActionResult DeleteTravel(TravelViewModel travel)
        {

            messageModel.Title = "Excluir Viagem";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var travelToDelete = db.Travel.Where(t => t.Id == travel.Id).FirstOrDefault();
            if (travelToDelete == null)
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            else
            {
                try
                {
                    var TravelUsers = db.TravelUser.Where(t => t.IdTravel == travel.Id).ToList();
                    foreach (var travelUser in TravelUsers)
                    {
                        foreach (var travelCost in travelUser.TravelUserCosts)
                        {
                            db.Cost.Remove(travelCost.Cost);
                        }
                        db.TravelUserCost.RemoveRange(travelUser.TravelUserCosts);
                    }
                    db.TravelUser.RemoveRange(TravelUsers);
                    db.Destination.RemoveRange(travelToDelete.Destinations);
                    db.Travel.Remove(travelToDelete);
                    db.SaveChanges();
                    messageModel.Message = "Viagem excluida com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        //OK
        public ActionResult DeleteInvite(int idTravel)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var travel = db.TravelUser.Where(t => t.IdTravel == idTravel && t.IsOwner).Select(t => t.Travel).FirstOrDefault();
            if (travel == null)
            {
                return RedirectToAction("Pendent");
            }
            TravelViewModel travelRefuse = AutoMapper.Mapper.Map<Travel, TravelViewModel>(travel);
            return View("_DeleteInvite", travelRefuse);
        }

        //OK
        [HttpPost]
        public ActionResult DeleteInvite(TravelViewModel travel)
        {

            messageModel.Title = "Recusar Convite de Viagem";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var travelToUpdate = db.TravelUser.Where(t => t.IdUser == userLogged.Id && t.IdTravel == travel.Id).FirstOrDefault();
            if (travelToUpdate == null)
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            else
            {
                try
                {
                    db.TravelUser.Remove(travelToUpdate);
                    db.SaveChanges();
                    messageModel.Message = "Convite recusado com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        //OK
        private void FillListContact(TravelUserViewCreate travel, UserViewItem userLogged)
        {
            var Contacts = db.Contact.Where(c => (c.IdRemittee == userLogged.Id || c.IdRequester == userLogged.Id) && !c.Pendent).ToList();
            var Users = GetContacts(Contacts, userLogged.Id);
            var travelUsers = db.TravelUser.Where(t => t.IdTravel == travel.IdTravel).Select(u => u.User).ToList();

            foreach (var item in travelUsers)
            {
                Users.Remove(item);
            }
            travel.Users = AutoMapper.Mapper.Map<List<User>, List<UserViewItem>>(Users);
        }

        //OK
        private List<User> GetContacts(List<Contact> Contacts, int idUser)
        {
            List<User> Users = new List<User>();
            if (Contacts != null && Contacts.Count > 0)
            {
                foreach (var item in Contacts)
                {
                    if (item.Remittee.Id != idUser)
                        Users.Add(item.Remittee);
                    else
                        Users.Add(item.Requester);
                }
            }
            return Users;
        }

        //OK
        private async Task FillLists(TravelViewCreate travel)
        {
            travel.States = await StateWS.GetStates();
            travel.Cities = new List<CityViewItem>();
        }

        //OK
        private bool VerifyCanBeAddTravel(DestinationViewModel Origin, DestinationViewModel Destination, UserViewItem userLogged)
        {
            var travelUserFromUser = db.TravelUser.Where(t => t.IdUser == userLogged.Id && !t.Pendent && !t.Travel.Pendent).Select(t => t.Travel).ToList();

            if (travelUserFromUser != null && travelUserFromUser.Count() > 0)
            {
                var mapped = AutoMapper.Mapper.Map<List<Travel>, List<TravelViewModel>>(travelUserFromUser);
                foreach (var item in mapped)
                {
                    if (item.Destinations[1].Date > Destination.Date)
                        return false;
                }
            }
            return true;
        }

        private decimal CalculateValue(List<Cost> Costs, int idUser, int countMember)
        {
            decimal myCostSum = 0;
            var costPerMember = Costs.Sum(t => t.Price)/countMember;
            var myCostList = Costs.Where(c => c.TravelUserCost.Where(t => t.IdTravelUserUser == idUser).Count() > 0).ToList();
            myCostList.ForEach(x => { myCostSum += (x.Price / x.TravelUserCost.Count()); });
            decimal myCostFinal = myCostSum - costPerMember;
            return myCostFinal;
        }
    }
}
