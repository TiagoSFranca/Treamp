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
    public class ContactController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;

        public ContactController()
        {
            //userLogged = (UserViewItem)HttpContext.Session["user"];
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AllContacts()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var ContactsView = new ContactViewList();
            var Contacts = db.Contact.Where(c => (c.IdRemittee == userLogged.Id || c.IdRequester == userLogged.Id)&& !c.Pendent);
            ContactsView.Contacts = AutoMapper.Mapper.Map<List<Contact>, List<ContactViewModel>>(Contacts.ToList());
            return PartialView("_AllContacts", ContactsView);
        }
                
        public ActionResult Requester()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            List<ContactViewModel> ContactsView = new List<ContactViewModel>();
            var Contacts = db.Contact.Where(c => c.IdRequester == userLogged.Id && c.Pendent);
            var ContactsMapped = AutoMapper.Mapper.Map<List<Contact>, List<ContactViewModel>>(Contacts.ToList());
            ContactsView = ContactsMapped;

            return PartialView("_ListRequester",ContactsView);
        }

        public PartialViewResult Remittee()
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            List<ContactViewModel> ContactsView = new List<ContactViewModel>();
            var Contacts = db.Contact.Where(c => c.IdRemittee == userLogged.Id && c.Pendent);
            var ContactsMapped = AutoMapper.Mapper.Map<List<Contact>, List<ContactViewModel>>(Contacts.ToList());
            ContactsView = ContactsMapped;

            return PartialView("_ListRemittee", ContactsView);
        }

        public ActionResult AddContact()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var contact = new AddContact();
            return View("_Add", contact);
        }

        [HttpPost]
        public ActionResult AddContact(AddContact contact)
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            if (!ModelState.IsValid) return View(contact);
            var user = VerifyContactExistsByEmail(contact.Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "E-mail não encontrado!");
                return PartialView("_Add", contact);
            }
            if(user.Id == userLogged.Id)
            {
                ModelState.AddModelError("Email", "Insira um e-mail diferente do seu!");
                return PartialView("_Add", contact);
            }
            var contactModel = new ContactViewItem();
            contactModel.IdRequester = userLogged.Id;
            contactModel.IdRemittee = user.Id;
            contactModel.Pendent = true;
            var contactMapped = AutoMapper.Mapper.Map<ContactViewItem, Contact>(contactModel);
            try
            {
                db.Contact.Add(contactMapped);
                db.SaveChanges();
                NotificationController.Create(user.Id, Services.AddUserMessage(userLogged.FullName));
                messageModel.Message = "Solicitação enviada com sucesso!";
                messageModel.Title = "Adicionar Contato";
            }
            catch(Exception e)
            {
                ModelState.AddModelError("Email", "Ocorreu um erro!");
                return PartialView("_Add", contact);
            }
            return View("_Message", messageModel);
        }

        public ActionResult DeleteContact(int idContact)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var contact = VerifyContactExistsByIdContact(idContact);
            if(contact == null)
            {
                return RedirectToAction("AllContacts");
            }
            ContactAddOrDelete contactDelete = FulFillContactAddOrDelete(contact);
            return View("_DeleteContact",contactDelete);
        }

        [HttpPost]
        public ActionResult DeleteContact(ContactAddOrDelete contactDelete)
        {
            messageModel.Title = "Excluir Contato";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var contact = VerifyContactExistsByIdContact(contactDelete.Id);
            if (contact == null)
            {

                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }else
            {
                try
                {
                    db.Contact.Remove(db.Contact.Find(contactDelete.Id));
                    db.SaveChanges();
                    messageModel.Message = "Contato excluido com sucesso!";
                }
                catch(Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        public ActionResult AceptContact(int idContact)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var contact = VerifyContactExistsByIdContact(idContact);
            if (contact == null)
            {
                return RedirectToAction("AllContacts");
            }
            ContactAddOrDelete contactAdd = FulFillContactAddOrDelete(contact);
            return View("_AceptContact", contactAdd);
        }

        [HttpPost]
        public ActionResult AceptContact(ContactAddOrDelete contactAdd)
        {
            messageModel.Title = "Aceitar Contato";
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var contact = VerifyContactExistsByIdContact(contactAdd.Id);
            if (contact == null)
            {

                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            else
            {
                try
                {
                    var contactUpdate = db.Contact.Find(contactAdd.Id);
                    contactUpdate.Pendent = false;
                    db.SaveChanges();
                    messageModel.Message = "Contato adicionado com sucesso!";
                }
                catch (Exception e)
                {
                    messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
                }
            }
            return View("_Message", messageModel);
        }

        private ContactAddOrDelete FulFillContactAddOrDelete(ContactViewModel contact)
        {
            ContactAddOrDelete contactAddOrDelete = new ContactAddOrDelete();
            contactAddOrDelete.Id = contact.Id;
            if (contact.IdRemittee == userLogged.Id)
            {
                contactAddOrDelete.Name = contact.Requester.FullName;
                contactAddOrDelete.Email = contact.Requester.Email;
            }
            else
            {
                contactAddOrDelete.Name = contact.Remittee.FullName;
                contactAddOrDelete.Email = contact.Remittee.Email;
            }
            return contactAddOrDelete;
        }
        
        private UserViewItem VerifyContactExistsByEmail(string email)
        {
            var user = db.User.Where(u => u.Email == email).FirstOrDefault();
            var userMapped = AutoMapper.Mapper.Map<User, UserViewItem>(user);
            return userMapped;
        }

        private ContactViewModel VerifyContactExistsByIdContact(int idContact)
        {
            userLogged = (UserViewItem)HttpContext.Session["user"];
            var contact = db.Contact.Where(c => c.Id == idContact && (c.IdRemittee == userLogged.Id || c.IdRequester == userLogged.Id)).FirstOrDefault();
            var contactMapped = AutoMapper.Mapper.Map<Contact, ContactViewModel>(contact);
            return contactMapped;
        }
    }
}
