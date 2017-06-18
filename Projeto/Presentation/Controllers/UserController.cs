using Presentation.Models;
using Presentation.Models.ViewModels;
using Presentation.Util;
using Presentation.WebServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private PresentationContext db = new PresentationContext();
        MessageViewModel messageModel = new MessageViewModel();
        UserViewItem userLogged;
        // GET: User
        public ActionResult Login()
        {
            return View(new UserViewLogin());
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }

        [HttpPost]
        public ActionResult Login(UserViewLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);
            UserViewItem user = new UserViewItem();

            if (!VerifyUserExists(userLogin, user))
            {
                ModelState.AddModelError("CustomError", "E-mail ou Senha não encontrados.");
                return View(userLogin);
            }
            Session["user"] = user;

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<ActionResult> Register()
        {
            var userRegister = new UserViewRegister();
            await FulFillLists(userRegister);
            return View("_Register", userRegister);
        }

        private async Task FulFillLists(UserViewRegister userRegister)
        {
            userRegister.Address.States = await StateWS.GetStates();
            userRegister.Address.Cities = new List<CityViewItem>();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserViewRegister user)
        {
            await FulFillLists(user);
            messageModel.Title = "Cadastro";
            if (!ModelState.IsValid) return View("_Register", user);
            if (VerifyUserExistsByEmail(user.Email))
            {
                ModelState.AddModelError("Email", "E-mail já cadastrado");
                return View("_Register", user);
            }
            if (VerifyUserExistsByCpf(user.Cpf))
            {
                ModelState.AddModelError("Cpf", "Cpf já cadastrado");
                return View("_Register", user);
            }
            var userMapped = AutoMapper.Mapper.Map<UserViewRegister, User>(user);
            userMapped.Password = Services.CalculateSHA1(userMapped.Password);
            try
            {
                var userAdded = db.User.Add(userMapped);
                var address = AutoMapper.Mapper.Map<AddressViewRegister, Address>(user.Address);
                address.IdUser = userAdded.Id;
                db.Address.Add(address);
                db.SaveChanges();
                messageModel.Message = "Cadastro realizado com sucesso!";
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "Ocorreu um erro.");
                return View("_Register", user);
            }
            //return RedirectToRoute(new { controller = "User", action = "Login" });
            return View("_Message", messageModel);
        }

        [HttpPost]
        public async Task<ActionResult> GetCities(int idState)
        {
            List<CityViewItem> objcity = new List<CityViewItem>();
            objcity = await CityWS.GetCities(idState);
            SelectList obgcity = new SelectList(objcity, "Id", "Name", 0);
            return Json(obgcity);
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult PersonalData()
        {
            var userLogged = (UserViewItem)Session["user"];
            return View("_PersonalData", userLogged);
        }

        public ActionResult ChangePassword()
        {
            return View("_ChangePassword", new ChangePasswordViewModel());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            messageModel.Title = "Alterar Senha";
            if (change.OldPassword == change.NewPassword)
                ModelState.AddModelError("NewPassword", "Nova senha não deve ser igual a atual.");
            if (!ModelState.IsValid)
                return View("_ChangePassword", change);

            var userLogged = (UserViewItem)Session["user"];

            var oldPassword = Services.CalculateSHA1(change.OldPassword);
            var result = db.User.Where(u => u.Id == userLogged.Id && u.Password == oldPassword).FirstOrDefault();
            if (result == null)
            {
                ModelState.AddModelError("OldPassword", "Senha atual errada");
                return View("_ChangePassword", change);
            }
            result.Password = Services.CalculateSHA1(change.NewPassword);
            try
            {
                db.SaveChanges();
                messageModel.Message = "Senha alterada com sucesso.";
            }
            catch (Exception e)
            {
                messageModel.Message = "Não foi possível concluir sua solicitação!\n Tente novamente mais tarde.";
            }
            return View("_Message", messageModel);
        }

        private bool VerifyUserExists(UserViewLogin user, UserViewItem userItem)
        {
            user.Password = Services.CalculateSHA1(user.Password);
            var result = db.User.Where(u => u.Email == user.Login && u.Password == user.Password).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            AutoMapper.Mapper.Map<User, UserViewItem>(result, userItem);
            return true;
        }

        private bool VerifyUserExistsByCpf(string cpf)
        {
            var user = db.User.Where(u => u.Cpf == cpf).FirstOrDefault();
            return (user != null) ? true : false;
        }

        private bool VerifyUserExistsByEmail(string email)
        {
            var user = db.User.Where(u => u.Email == email).FirstOrDefault();
            return (user != null) ? true : false;
        }
    }
}