using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Repo;

namespace WebTicket.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        public ActionResult LoginData(UserModel newUser)
        {
            var handler = new UserRepo();
            var user = handler.Get(x => x.RuiJieId == newUser.RuiJieId);
            if (user.Password == newUser.Password)
            {
                return RedirectToAction("index", "User");
            }
            else
            {
                return RedirectToAction("error", "User");
            }

        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SaveRegisterDate(UserModel newUser)
        {
            var handler = new UserRepo();
            handler.Add(newUser);
            return RedirectToAction("Login", "User");
        }
        //public ActionResult AdministratorRegister()
        //{
        //    return View(); 
        //}

        //public ActionResult AdministratorRegisterData(AdministratorModel newAdministrator)
        //{
        //    var handler = new UserRepo();
        //    handler.Add(newAdministrator);
        //    return RedirectToAction("Login", "User");

        //}
        //public ActionResult MaintainerRegister()
        //{
        //    return View();
        //}

        //public ActionResult MaintainerRegisterData(MaintainerModel newMaintainer)
        //{
        //    var handler = new UserRepo();
        //    handler.Add(newMaintainer);
        //    return RedirectToAction("Login", "User");

        //}
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult FindPassword(UserModel newUser)
        {
            var handler = new UserRepo();

            return RedirectToAction("", "");


        }
    }
}