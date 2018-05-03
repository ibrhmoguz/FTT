using System;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Abstract;
using System.Web.Mvc;
using Gov.GTB.FirmaTalepTakip.Model.Enums;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Web.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IUserRepository userRepository;

        public AccountController(IAuthProvider auth, IUserRepository userRepository)
        {
            this.authProvider = auth;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var kullanici = userRepository.KullanicilariGetir().FirstOrDefault(x => x.TcNo == Convert.ToInt64(model.TcNo));
                if (kullanici != null)
                {
                    Session["CurrentUserId"] = kullanici.TcNo;
                    Session["CurrentUserName_SurName"] = kullanici.Adi + " " + kullanici.Soyadi;
                    RolEnum kullaniciRol;
                    Enum.TryParse(kullanici.RolId.ToString(), out kullaniciRol);
                    Session["CurrentUser_Auths"] = new KullaniciYetkileri { KullaniciRolEnum = kullaniciRol };

                    authProvider.Authenticate(model.TcNo, model.Password);
                    return Redirect(returnUrl ?? Url.Action("Index", "Default"));
                }

                ModelState.AddModelError("IncorrectInfo", "Incorrect user name or password!");
                return View();
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Clear();
            authProvider.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}