using Presentation.Models;
using Presentation.Models.ViewModels;
using Presentation.WebServiceReference;
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
            var bk = db.Address.FirstOrDefault(b => b.IdUser == userLogged.Id);
            AddressViewEdit bank = AutoMapper.Mapper.Map<Address, AddressViewEdit>(bk);
            bank.City = await CityWS.GetCity((int)bank.IdCity);
            await FulFillLists(bank);
            return View("_EditAddress", bank);
        }

        [HttpPost]
        public ActionResult Edit(AddressViewEdit address)
        {
            return View();
        }

        private async Task FulFillLists(AddressViewEdit addressRegister)
        {
            addressRegister.States = await StateWS.GetStates();
            addressRegister.Cities = new List<CityViewItem>();
        }
    }
}