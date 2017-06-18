using Presentation.Models;
using Presentation.Models.ViewModels;
using Presentation.WebServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class AddressController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;

        // GET: Address
        public async Task<ActionResult> Index()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var bk = db.Address.FirstOrDefault(b => b.IdUser == userLogged.Id);
            AddressViewModel bank = AutoMapper.Mapper.Map<Address, AddressViewModel>(bk);
            bank.City = await CityWS.GetCity(bank.IdCity);
            return View("Index", bank);
        }

        public async Task<ActionResult> Edit()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var ads = db.Address.FirstOrDefault(b => b.IdUser == userLogged.Id);
            AddressViewEdit address = AutoMapper.Mapper.Map<Address, AddressViewEdit>(ads);
            address.City = await CityWS.GetCity((int)address.IdCity);
            await FulFillLists(address);
            return View("_EditAddress", address);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AddressViewEdit address)
        {
            messageModel.Title = "Editar Endereço";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            if (!ModelState.IsValid)
            {
                address.City = await CityWS.GetCity((int)address.IdCity);
                await FulFillLists(address);
                return View("_EditAddress", address);
            }
            try
            {
                var ads = db.Address.FirstOrDefault(b => b.IdUser == userLogged.Id);
                ads.District = address.District;
                ads.Number = address.Number;
                ads.Street = address.Street;
                if (address.IdCity != null)
                {
                    ads.IdCity = (int)address.IdCity;
                }
                db.SaveChanges();
                messageModel.Message = "Endereço alterado com sucesso!";
            }
            catch (Exception e)
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            return View("_Message", messageModel);
        }

        private async Task FulFillLists(AddressViewEdit addressRegister)
        {
            addressRegister.States = await StateWS.GetStates();
            addressRegister.Cities = new List<CityViewItem>();
        }
    }
}