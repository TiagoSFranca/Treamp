using Presentation.Models;
using Presentation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class BankAccountController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;
        // GET: BankAccount
        public ActionResult AddAccount()
        {
            BankAccountViewModel bank = new BankAccountViewModel();
            FulFillLists(bank);
            return View("_AddAccount", bank);
        }

        [HttpPost]
        public ActionResult AddAccount(BankAccountViewModel bank)
        {
            if (!ModelState.IsValid)
            {
                FulFillLists(bank);
                return View("_AddAccount", bank);
            }
            return View("_AddAccount", bank);
        }

        private void FulFillLists(BankAccountViewModel bank)
        {
            var listTypeAcc = db.TypeAccount.ToList();
            bank.TypeAccountList = AutoMapper.Mapper.Map<List<TypeAccount>, List<TypeAccountViewItem>>(listTypeAcc);
        }
    }
}