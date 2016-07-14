using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BugTrackingSystem.Service.Services;
using BugTrackingSystem.Web.Filters;
using UserRole = BugTrackingSystem.Service.Models.UserRole;
using BugTrackingSystem.Data.Model;

namespace BugTrackingSystem.Web.Controllers
{
    [CustomAuthenticate]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            string headerToken = "";
            if (ModelState.IsValid)
            {
                //TODO Check in DB for existing and valid user
                if (_userService.IsUserExists(model.Email, model.Password))
                {
                    //model.UserRoleID = 2;
                    //if (_userService.GetUserRoleByEmail(model.Email) == UserRole.Administrator)
                    //{
                    //    model.UserRoleID = 1;
                    //}
                    //else if (_userService.GetUserRoleByEmail(model.Email) == UserRole.SuperUser)
                    //{
                    //    model.UserRoleID = 3;
                    //}
                    var userToken = new FormsAuthenticationTicket(1, model.Email, DateTime.Now,
                        DateTime.Now.AddMinutes(10), false, model.Email == "chuiguk.eu@gmail.com" ? "admin" : "user");

                    headerToken = FormsAuthentication.Encrypt(userToken);
                }

                if (!string.IsNullOrEmpty(headerToken))
                    {
                        Response.Cookies.Add(new HttpCookie("auth", headerToken));

                        return string.IsNullOrEmpty(returnUrl) ? Redirect("/") : Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "failed to create token");
                    }
                
            }

            return View(model);
        }

        [CustomAuthorize]
        public ActionResult Logout()
        {
            Response.Cookies.Add(new HttpCookie("auth", null));
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
    }
}