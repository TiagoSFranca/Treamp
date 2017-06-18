using Presentation.Models;
using Presentation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class PhoneController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;

        public ActionResult Index()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var pn = db.Phone.Where(b => b.IdUser == userLogged.Id).ToList();
            List<PhoneViewModel> phone = AutoMapper.Mapper.Map<List<Phone>, List<PhoneViewModel>>(pn);
            return View("Index", phone);
        }

        public ActionResult AddPhone()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            PhoneViewModel phone = new PhoneViewModel();
            return View("_AddPhone", phone);
        }

        [HttpPost]
        public ActionResult AddPhone(PhoneViewModel phone)
        {
            messageModel.Title = "Cadastrar Telefone";

            userLogged = (UserViewItem)HttpContext.Session["user"];
            phone.IdUser = userLogged.Id;
            if (VerifyPhoneExists(phone) != null)
            {
                ModelState.AddModelError("Number", "Telefone já cadastrado");
            }

            if (!ModelState.IsValid)
            {
                return View("_AddPhone", phone);
            }
            if (!CanCreatePhone(userLogged.Id))
            {
                messageModel.Message = "Você já possui o máximo de telefones permitidos: 2";
            }
            else
            {
                try
                {
                    var bankMapped = AutoMapper.Mapper.Map<PhoneViewModel, Phone>(phone);
                    db.Phone.Add(bankMapped);
                    db.SaveChanges();
                    messageModel.Message = "Telefone adicionado com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        public ActionResult DeletePhone(int id)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            userLogged = (UserViewItem)HttpContext.Session["user"];
            if (VerifyPhoneExistsByIdUser(userLogged.Id, id))
            {
                var result = db.Phone.FirstOrDefault(t => t.Id == id);
                PhoneViewModel bank = AutoMapper.Mapper.Map<Phone, PhoneViewModel>(result);
                return View("_DeletePhone", bank);
            }
            else
            {
                return RedirectToAction("Profile", "User");
            }
        }

        [HttpPost]
        public ActionResult DeletePhone(PhoneViewModel phone)
        {
            messageModel.Title = "Excluir Telefone";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            if (VerifyPhoneExistsByIdUser(userLogged.Id, phone.Id))
            {
                var result = db.Phone.Find(phone.Id);
                try
                {
                    db.Phone.Remove(result);
                    db.SaveChanges();
                    messageModel.Message = "Telefone excluido com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            else
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            return View("_Message", messageModel);
        }

        private bool VerifyPhoneExistsByIdUser(int idUser, int idAccount)
        {
            var result = db.Phone.FirstOrDefault(t => t.Id == idAccount && t.IdUser == idUser);
            return (result != null);
        }

        private Phone VerifyPhoneExists(PhoneViewModel bank)
        {
            var acc = db.Phone.Where(b => b.Number == bank.Number && b.Prefix == bank.Prefix).FirstOrDefault();
            return acc;
        }

        private bool CanCreatePhone(int idUser)
        {
            var countPhone = db.Phone.Where(b => b.IdUser == idUser).Count();
            return (countPhone < 2);
        }
        
    }
}