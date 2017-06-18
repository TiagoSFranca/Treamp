using Presentation.Models;
using Presentation.Models.ViewModels;
using Presentation.WebServiceReference;
using System.Linq;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class AddressController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;

        // GET: Address
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var bk = db.Address.FirstOrDefault(b => b.IdUser == userLogged.Id);
            AddressViewModel bank = AutoMapper.Mapper.Map<Address, AddressViewModel>(bk);
            bank.City = await CityWS.GetCity(bank.IdCity);
            return View("Index", bank);
        }
    }
}