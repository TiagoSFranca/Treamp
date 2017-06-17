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
            messageModel.Title = "Cadastrar Conta";

            userLogged = (UserViewItem)HttpContext.Session["user"];
            bank.IdUser = userLogged.Id;
            if(VerifyAccountExists(bank) != null)
            {
                ModelState.AddModelError("Account", "Conta já existe");
            }

            if (!ModelState.IsValid)
            {
                FulFillLists(bank);
                return View("_AddAccount", bank);
            }
            if (!CanCreateAccount(userLogged.Id))
            {
                messageModel.Message = "Você já possui o máximo de contas permitidas: 2";
            }else
            {
                try
                {
                    var bankMapped = AutoMapper.Mapper.Map<BankAccountViewModel, BankAccount>(bank);
                    db.BankAccount.Add(bankMapped);
                    db.SaveChanges();
                    messageModel.Message = "Conta bancária adicionada com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        private BankAccount VerifyAccountExists(BankAccountViewModel bank)
        {
            var acc = db.BankAccount.Where(b => b.Account == bank.Account && b.Agency == bank.Agency && b.Name == bank.Name).FirstOrDefault();
            return acc;
        }

        private bool CanCreateAccount(int idUser)
        {
            var countAcc = db.BankAccount.Where(b => b.IdUser == idUser).Count();
            return (countAcc < 2);
        }

        private void FulFillLists(BankAccountViewModel bank)
        {
            var listTypeAcc = db.TypeAccount.ToList();
            bank.TypeAccountList = AutoMapper.Mapper.Map<List<TypeAccount>, List<TypeAccountViewItem>>(listTypeAcc);
        }
    }
}