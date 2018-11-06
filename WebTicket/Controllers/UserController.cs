using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Repo;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Newtonsoft.Json;

namespace WebTicket.Controllers
{
    public class UserController : Controller
    {


        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        public ActionResult LoginData(UserModel newUser, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                //newUser.Name = newUser.Name.Trim();
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] originalBytes = Encoding.Default.GetBytes(newUser.Password);
                byte[] encodedBytes = md5.ComputeHash(originalBytes);
                newUser.Password = BitConverter.ToString(encodedBytes);
                var handler = new UserRepo();
                var user = handler.Get(x => x.RuiJieId == newUser.RuiJieId);
                if (user.Password == newUser.Password)
                {
                    FormsAuthentication.SetAuthCookie(newUser.Name, newUser.IsRememberMe);
                    return this.CheckReturnUrl(returnUrl)
                        ? this.Redirect(returnUrl)
                        : this.RedirectToAction("index", "User") as ActionResult;
                }
                this.ModelState.AddModelError("", "请检查你的用户名或密码");
            }
            return this.View("Index", newUser);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index");
        }

        private static void SetAuthCookie(
            string userName,
            bool createPersistentCookie,
            object userData)
        {
            if (!System.Web.HttpContext.Current.Request.IsSecureConnection && FormsAuthentication.RequireSSL)
            {
                throw new HttpException("Connection not secure creating secure cookie");
            }

            // <!> In this way, we will lose the function of cookieless
            //var flag = UseCookieless(
            //    System.Web.HttpContext.Current,
            //    false,
            //    FormsAuthentication.CookieMode);

            FormsAuthentication.Initialize();
            if (userName == null)
            {
                userName = string.Empty;
            }
            var cookiePath = FormsAuthentication.FormsCookiePath;
            var utcNow = DateTime.UtcNow;
            var expirationUtc = utcNow + FormsAuthentication.Timeout;
            var authenticationTicket = new FormsAuthenticationTicket(
                2,
                userName,
                utcNow.ToLocalTime(),
                expirationUtc.ToLocalTime(),
                createPersistentCookie,
                JsonConvert.SerializeObject(userData),
                cookiePath
            );

            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            if (string.IsNullOrEmpty(encryptedTicket))
            {
                throw new HttpException("Unable to encrypt cookie ticket");
            }
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Path = cookiePath,
                Secure = FormsAuthentication.RequireSSL
            };

            if (FormsAuthentication.CookieDomain != null)
            {
                authCookie.Domain = FormsAuthentication.CookieDomain;
            }
            if (authenticationTicket.IsPersistent)
            {
                authCookie.Expires = authenticationTicket.Expiration;
            }

            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        private bool CheckReturnUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            // Make Sure the return url was not redirect to an external site.
            if (Uri.TryCreate(url, UriKind.Absolute, out var absoluteUri))
            {
                return string.Equals(
                    this.Request.Url.Host,
                    absoluteUri.Host, StringComparison.OrdinalIgnoreCase);
            }
            return url[0] == '/' && (url.Length == 1
                                     || url[1] != '/' && url[1] != '\\')
                   || url.Length > 1 && url[0] == '~' && url[1] == '/';
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SaveRegisterDate(UserModel newUser)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = Encoding.Default.GetBytes(newUser.Password);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);
            newUser.Password = BitConverter.ToString(encodedBytes);
            newUser.ConfirmPassword = BitConverter.ToString(encodedBytes);
            var handler = new UserRepo();
            handler.Add(newUser);
            return RedirectToAction("Login", "User");
        }
        public ActionResult Accredit()
        {
            return View();

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